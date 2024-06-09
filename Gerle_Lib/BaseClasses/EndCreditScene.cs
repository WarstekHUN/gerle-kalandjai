using Gerle_Lib.Data;

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

        private void PlayMusic(CancellationToken token)
        {
            throw new NotImplementedException();
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
