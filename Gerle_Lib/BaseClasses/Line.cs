#region Line (osztály) - comment
/// <summary>
/// A forgatókönyv osztálya (<c>Line.cs</c>). A játékmenet során 1-1 jelenet/párbeszéd sorokat tesz ki,
/// ennek mind megvannak a szerplői hangfáljai amik abban a sorban szerepelnek, lejátszásra kerülnek.
/// </summary>
#endregion
public class Line
{
    #region Text (tulajdonság) - comment
    /// <summary>
    /// <c>Text</c> tulajdonság a kiírandó sor szövegét tartalmazza.
    /// </summary>
    #endregion
    public string Text { get; set; }
    //TODO: Ennel legyen az alapértelmezett értéke a Narrátor Actor
    #region Talker (tulajdonság) - comment
    /// <summary>
    /// <c>Talker</c> tulajdonság a sor beszélőjét / Actor-ját tartalmazza (Actor.cs). Ha nincsen Actor-ja a sornak, akkor a sor a narrátort illeti.
    /// Az Actor kifejezés azt játékon belüli karaktert, szereplőt vagy narrátort jelképezi, aki a sorban szerepel.
    /// </summary>
    #endregion
    public Actor Talker { get; set; }
    #region VoiceFile (tulajdonság) - comment
    /// <summary>
    /// <c>VoiceFile</c> tulajdonság a hangfelvétel fájljának a nevét tartalmazza. Pl: "felvetel.wav"
    /// </summary>
    #endregion
    public string VoiceFile { get; set; }
    #region NoiseFile (tulajdonság) - comment
    /// <summary>
    /// <c>NoiseFile</c> tulajdonság a háttérzaj fájljának a nevét tartalmazza. Ezek lehetnek speciális effektek (FX). Pl: "hatterzaj.wav"
    /// </summary>
    #endregion
    public string? NoiseFile { get; set; }
    #region Line (paraméteres konstruktor) - comment
    /// <summary>
    /// <c>Line</c> paraméteres konstruktor a fentebb megadott tulajdonságoknak adja meg a beállítandó értékeket.
    /// </summary>
    #endregion
    public Line(string text, ref Actor talker, string voiceFile, string? noiseFile = null)
    {
        Text = text;
        Talker = talker;
        VoiceFile = voiceFile;
        NoiseFile = noiseFile;
    }
    #region PlayLine (metódus) - comment
    /// <summary>
    /// <c>PlayLine</c> metódus az adott / lejátszandó sort játsza le a fent megadott tulajdonságok értékeivel.
    /// </summary>
    #endregion
    public void PlayLine()
    {
        throw new NotImplementedException();
    }
}
