using Gerle_Lib.Controllers;
using NAudio.Wave;

namespace Gerle_Lib.BaseClasses
{
    class EndCreditScene : Scene
    {
        private string MusicFile { get; init; }
        private EndCredit[] EndCredits {  get; init; }

        public EndCreditScene(EndCredit[] credits, string musicFile, SceneVersion choiceVersion = SceneVersion.BASE) : base(new Line[] { }, musicFile, choiceVersion)
        {
            MusicFile = musicFile;
            EndCredits = credits;
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

        public override SceneVersion? PlayScene()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            Task.Run(() => PlayMusic(tokenSource.Token));

            foreach (EndCredit credit in EndCredits)
            {
                //Renderelés
                Thread.Sleep(3500);
            }

            tokenSource.Cancel();

            return null;

        }
    }
}
