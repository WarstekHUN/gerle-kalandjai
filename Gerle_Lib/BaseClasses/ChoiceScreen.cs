using Gerle_Lib.Data;

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

        private void PlayNarratorVoice(CancellationToken token)
        {
            throw new NotImplementedException();
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
