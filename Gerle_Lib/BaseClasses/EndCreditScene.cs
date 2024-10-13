using Gerle_Lib.Controllers;
using NAudio.Wave;
using Gerle_Lib.UIReleated;

namespace Gerle_Lib.BaseClasses
{
    class EndCreditScene : Scene
    {
        private string MusicFile { get; init; }
        private EndCredit[] EndCredits { get; init; }
        private ushort MusicDurationInSec { get; init; }

        public EndCreditScene(EndCredit[] credits, string musicFile, ushort musicDurationInSec, SceneVersion choiceVersion = SceneVersion.BASE) : base(new Line[] { }, musicFile, choiceVersion)
        {
            MusicFile = Path.Join("Data/Audio/Music/", musicFile + ".mp3");
            EndCredits = credits;
            MusicDurationInSec = musicDurationInSec;
        }

        private async void PlayMusic(CancellationToken token)
        {
            TaskCompletionSource completionSource = new TaskCompletionSource();

            WaveOutEvent player = new WaveOutEvent();

            using(Mp3FileReader reader = new Mp3FileReader(MusicFile))
            {
                player.Init(reader);
                player.Volume = SettingsController.MasterVolume * SettingsController.MusicVolume;
                player.Play();

                token.Register(player.Stop);

                player.PlaybackStopped += (object? sender, StoppedEventArgs e) =>
                {
                    if (e.Exception is not null) throw e.Exception;
                    reader.Dispose();
                    completionSource.SetResult();
                };

                await completionSource.Task;
            }
        }

        public override async Task<SceneVersion> PlayScene()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => PlayMusic(tokenSource.Token));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            UI.EndCreditUI(EndCredits, MusicDurationInSec);

            Console.WriteLine("Nyomj meg egy gombot a kilépéshez!");
            Console.ReadKey();
            
            tokenSource.Cancel();

            return SceneVersion.BASE;
        }
    }
}
