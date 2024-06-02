namespace Gerle_Lib.BaseClasses
{
    public class SceneMusic
    {
        public string MusicBegining { get; init; }
        public string MusicBase { get; init; }
        public string MusicEnding { get; init; }
        public byte BPM { get; init; }

        /// <summary>
        /// Harc alatt lejátszott zene. A paramétereknek csak a fájlnév kell. Sem elérési útat, sem kiterjesztést (mp3) nem szabad megadni.
        /// </summary>
        /// <param name="musicBegining"></param>
        /// <param name="musicBase"></param>
        /// <param name="musicEnding"></param>
        /// <param name="BPM"></param>
        public SceneMusic(string musicBegining, string musicBase, string musicEnding, byte BPM)
        {
            MusicBegining = Path.Join("Data/Audio/Music", musicBegining + ".mp3");
            MusicBase = Path.Join("Data/Audio/Music", musicBase + ".mp3");
            MusicEnding = Path.Join("Data/Audio/Music", musicEnding + ".mp3");
            this.BPM = BPM;
        }
    }
}
