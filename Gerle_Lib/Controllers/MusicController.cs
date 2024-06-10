using Gerle_Lib.BaseClasses;
using NAudio.Utils;
using NAudio.Wave;

namespace Gerle_Lib.Controllers
{
    public static class MusicController
    {
        public static SceneMusic? CurrentlyPlaying { get; private set; }
        private static WaveOutEvent? CurrentPlayer { get; set; }
        private static CancellationTokenSource? CancelSource { get; set; }

        /// <summary>
        /// Azt az időt adja meg másodpercben, amíg a MusicController lekeveri egy zene hangerejét
        /// </summary>
        private const float FADE_OUT_TIME_S = 1.5f;

        public static async void PlayMusic(SceneMusic music)
        {
            CancelSource = new CancellationTokenSource();
            CurrentlyPlaying = music;
            await Task.Run(() => _PlayOnce(CancelSource.Token, CurrentlyPlaying.MusicBegining));

            if (CancelSource.IsCancellationRequested) return;

            _ = Task.Run(() => _PlayRepeat(CancelSource.Token, CurrentlyPlaying.MusicBase));
        }

        /// <summary>
        /// Elhalkítja a zenét megadott idő alatt
        /// </summary>
        /// <param name="fadeOutTime">A fadelés ideje másodpercben</param>
        private static void FadeOutMusic(float fadeOutTime = FADE_OUT_TIME_S)
        {
            if (CurrentPlayer is null || CurrentlyPlaying is null) return;

            float volumeFadeBySec = 1.0f / fadeOutTime;

            DateTime lastTick = DateTime.Now;

            while (CurrentPlayer.Volume > 0)
            {
                DateTime now = DateTime.Now;
                CurrentPlayer.Volume -= Math.Max((float)new TimeSpan(now.Ticks - lastTick.Ticks).TotalSeconds * volumeFadeBySec, 0);
                lastTick = now;
            }
        }

        /// <summary>
        /// Elkezdi lehalkíteni a jelenleg lejátszott zenát, ezel egyidejűleg a másik hangerejét ugyanolyan gyorsan felviszi.
        /// A másik zene mindig az EndingMusic. Ezt a függvényt akkor kell használni, ha a játékos megnyerte a csatát.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="fadeOutTime"></param>
        private static async void CrossFade(string filepath, float fadeOutTime = FADE_OUT_TIME_S)
        {
            if (CurrentPlayer is null || CurrentlyPlaying is null) return;

            Mp3FileReader reader = new Mp3FileReader(filepath);
            
                //Fájl felkészítése, betöltése RAM-ba.
                WaveOutEvent player = new WaveOutEvent();
                player.Init(reader);
                float targetVolume = SettingsController.MusicVolume * SettingsController.MasterVolume;
                player.Volume = 0f;

                CancelSource?.Token.Register(player.Stop);

                float volumeFadeBySec = 1.0f / fadeOutTime;

                //Hány másodpercenként van egy nyolcas beat: 1 mp / Hány nyolcas beat van másodpercenként
                float secondsPerOctaveBeat = 1 / (CurrentlyPlaying.BPM / 60 / 8);

                //Kiszámolom, hogy mikor lesz a következő utáni nyolcas beat a zenében
                float nextBeatSec = (MathF.Ceiling((float)CurrentPlayer.GetPositionTimeSpan().TotalSeconds / secondsPerOctaveBeat) + 1) * secondsPerOctaveBeat;

                while(CurrentPlayer.GetPositionTimeSpan().TotalSeconds < nextBeatSec)
                {
                    //Várok a jó beatre a zenében
                    Thread.Sleep(1);
                }

#pragma warning disable CS4014 // Normális, hogy nincs awaitelve, mert ennek a szálnak a háttérben kell futnia, amíg felkeverő szál fut
                Task.Run(() => FadeOutMusic());
#pragma warning restore CS4014

                //Tervezési szempontból jobb lenne ezt egy külön függvénybe pakolni, de az csak úgy lenne szép, ha egyes dolgokat nem számolnék ki előre. Ha ezt nem tenném meg, akkor késne a Task elindítása, ezzel pedig kicsit szétcsúszna a crossfade effekt.
               await Task.Run(() =>
                {
                    TaskCompletionSource source = new TaskCompletionSource();
                    player.Play();
                    DateTime lastTick = DateTime.Now;

                    while (player.Volume < targetVolume && CancelSource is not null && !CancelSource.IsCancellationRequested)
                    {
                        DateTime now = DateTime.Now;
                        player.Volume += Math.Min((float)new TimeSpan(now.Ticks - lastTick.Ticks).TotalSeconds * volumeFadeBySec, 1);
                        lastTick = now;
                    }

                    return player;
                });

                CurrentPlayer.Dispose();
                CurrentPlayer = player;
            
        }

        /// <summary>
        /// Akkor kell meghívni, ha a játékos elveszti a csatát
        /// </summary>
        public static void StopMusic()
        {
            CancelSource?.Cancel();

            CurrentlyPlaying = null;
            CurrentPlayer = null;
            CancelSource = null;
        }

        /// <summary>
        /// Akkor kell meghívni, amikor a játkos megnyeri a csatát
        /// </summary>
        public static void EndMusic()
        {
            if (CurrentlyPlaying is null || CurrentPlayer is null) return;
            CancelSource = new CancellationTokenSource();
            CrossFade(CurrentlyPlaying.MusicEnding);
        }

        /// <summary>
        /// Elkezd lejátszani egy zenét. Hogyha vége lesz, újraindul.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="startVolume"></param>
        /// <returns></returns>
        private static void _PlayRepeat(CancellationToken cancellationToken, string path, float? startVolume = null)
        {
            AutoResetEvent waitHandle = new AutoResetEvent(false);

            Mp3FileReader file = new Mp3FileReader(path);
            
                if (CurrentPlayer != null)
                {
                    CurrentPlayer.Dispose();
                }

                WaveOutEvent localPlayer = new WaveOutEvent();
                localPlayer.Init(file);
                localPlayer.Volume = startVolume * SettingsController.MasterVolume ?? SettingsController.MusicVolume * SettingsController.MasterVolume;
                localPlayer.Play();

                cancellationToken.Register(() =>
                {
                    waitHandle.Set();
                    localPlayer.PlaybackStopped -= eventHandler;
                    localPlayer.Dispose();
                    file.Dispose();
                });

                void eventHandler(object? sender, StoppedEventArgs ev)
                {
                    file.Position = 0;
                    localPlayer.Play();
                };

                //Amennyiben van egy kis szünet a zene vége és az újraindulás között, egy új Thread-et kell létrehozni már 80%-nál, amire átváltunk, ha véget ér a zene.
                localPlayer.PlaybackStopped += eventHandler;
 
                CurrentPlayer = localPlayer;
                waitHandle.WaitOne();
            
        }

        /// <summary>
        /// Lejátsza a megadott zenét egyszer
        /// </summary>
        /// <param name="path"></param>
        /// <param name="startVolume"></param>
        /// <returns></returns>
        private static void _PlayOnce(CancellationToken cancellationToken, string path, float? startVolume = null)
        {
            AutoResetEvent waitHandle = new AutoResetEvent(false);

            Mp3FileReader file = new Mp3FileReader(path);
                if (CurrentPlayer != null)
                {
                    CurrentPlayer.Dispose();
                }

                WaveOutEvent localPlayer = new WaveOutEvent();
                localPlayer.Init(file);
                localPlayer.Volume = startVolume * SettingsController.MasterVolume ?? SettingsController.MusicVolume * SettingsController.MasterVolume;
                localPlayer.Play();

                cancellationToken.Register(localPlayer.Stop);

                localPlayer.PlaybackStopped += (sender, ev) =>
                {
                    if (ev.Exception is not null) return;
                    localPlayer.Dispose();
                    file.Dispose();
                    waitHandle.Set();
                };

                CurrentPlayer = localPlayer;

                waitHandle.WaitOne();
        }
    }
}
