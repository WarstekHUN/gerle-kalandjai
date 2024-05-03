public class Line
{
    public string Text { get; set; }
    //TODO: Ennel legyen az alapértelmezett értéke a Narrátor Actor
    public Actor Talker { get; set; }
    public string VoiceFile { get; set; }
    public string NoiseFile { get; set; }

    public Line(string text, ref Actor talker, string voiceFile, string noiseFile)
    {
        Text = text;
        Talker = talker;
        VoiceFile = voiceFile;
        NoiseFile = noiseFile;
    }

    public void PlayLine()
    {
        throw new NotImplementedException();
    }
}
