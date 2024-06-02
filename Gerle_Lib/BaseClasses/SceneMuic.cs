namespace Gerle_Lib.BaseClasses
{
    public class SceneMuic
    {
        public string MusicBegining { get; init; }
        public string MusicBase { get; init; }
        public string MusicEnding { get; init; }
        public byte BPM { get; init; }

        public SceneMuic(string musicBegining, string musicBase, string musicEnding, byte BPM)
        {
            MusicBegining = musicBegining;
            MusicBase = musicBase;
            MusicEnding = musicEnding;
            this.BPM = BPM;
        }
    }
}
