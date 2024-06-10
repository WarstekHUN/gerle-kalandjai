using Gerle_Lib.Controllers;
using Gerle_Lib.Data;
using NAudio.Wave;

namespace Gerle_Lib.BaseClasses
{
    public class ChoiceScreen : Line
    {
        #region Choices (tulajdonság)
        /// <summary>
        /// Tartalmazza a játékos választási lehetőségeit
        /// </summary>
        #endregion
        public Choice[] Choices { get; init; }

        public ChoiceScreen(Choice[] choices, string voiceFile, string? noiseFile = null) : base("Választási lehetőség", ref Actors.Narrator, voiceFile)
        {
            Choices = choices;
        }

        private async void PlayNarratorVoice(CancellationToken token)
        {
            TaskCompletionSource completionSource = new TaskCompletionSource();

            using(Mp3FileReader reader = new Mp3FileReader(VoiceFile))
            {
                WaveOutEvent player = new WaveOutEvent();
                player.Init(reader);
                player.Volume = SettingsController.MasterVolume * SettingsController.DialogueVolume;
                player.Play();

                player.PlaybackStopped += (object? sender, StoppedEventArgs e) =>
                {
                    if(e.Exception != null) throw e.Exception;

                    player.Dispose();
                    reader.Dispose();
                    completionSource.SetResult();
                };
            }

            await completionSource.Task;
        }

        #region PresentChoiceToPlayer (függvény)
        /// <summary>
        /// Kirendereli a választási képernyőt, hogy a játékos el tudja dönteni, merre menjen tovább a történet
        /// </summary>
        /// <returns>A játékos választása</returns>
        #endregion
        public Choice PresentChoiceToPlayer()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Task.Run(() => PlayNarratorVoice(tokenSource.Token));


            tokenSource.Cancel();

            throw new NotImplementedException();
        }
    }
}
