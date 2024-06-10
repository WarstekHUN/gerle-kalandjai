#region Line (osztály) - comment
#endregion
using Gerle_Lib.Controllers;
using Gerle_Lib.UIReleated;
using NAudio.Wave;

/// <summary>
/// A forgatókönyv osztálya (<c>Line.cs</c>). A játékmenet során 1-1 jelenet/párbeszéd sorokat tesz ki,
/// ennek mind megvannak a szerplői hangfáljai amik abban a sorban szerepelnek, lejátszásra kerülnek.
/// </summary>
public class Line
{
    #region Text (tulajdonság) - comment
    /// <summary>
    /// <c>Text</c> tulajdonság a kiírandó sor szövegét tartalmazza.
    /// </summary>
    #endregion
    public string Text { get; init; }
    #region Talker (tulajdonság) - comment
    /// <summary>
    /// <c>Talker</c> tulajdonság a sor beszélőjét / Actor-ját tartalmazza (Actor.cs). Ha nincsen Actor-ja a sornak, akkor a sor a narrátort illeti.
    /// Az Actor kifejezés azt játékon belüli karaktert, szereplőt vagy narrátort jelképezi, aki a sorban szerepel.
    /// </summary>
    #endregion
    public Actor Talker { get; init; }
    #region VoiceFile (tulajdonság) - comment
    /// <summary>
    /// <c>VoiceFile</c> tulajdonság a hangfelvétel fájljának a nevét tartalmazza. Pl: "felvetel.wav"
    /// </summary>
    #endregion
    public string VoiceFile { get; init; }
    #region NoiseFile (tualjdonság)
    /// <summary>
    /// Az adott elmondott sor alatt lejátszott hangfájl.
    /// Pl: Amikor Jézus beszél, a háttérben ilyen szent zene szól.
    /// </summary>
    #endregion
    public string? NoiseFile { get; init; }
    
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

    internal async Task PlayAudioFile(string filePath, CancellationToken? token = null)
    {
        TaskCompletionSource source = new TaskCompletionSource();
        using (Mp3FileReader reader = new Mp3FileReader(filePath))
        {
            WaveOutEvent player = new WaveOutEvent();
            player.Init(reader);
            player.Volume = SettingsController.MasterVolume * SettingsController.MusicVolume;
            player.Play();

            token?.Register(player.Stop);

            player.PlaybackStopped += (object? sender, StoppedEventArgs e) =>
            {
                if(e.Exception is not null) source.SetException(e.Exception);
                player.Dispose();
                reader.Dispose();
                source.SetResult();
            };
        }

        await source.Task;
    }

    #region PlayLine (metódus) - comment
    /// <summary>
    /// <c>PlayLine</c> metódus az adott / lejátszandó sort játsza le a fent megadott tulajdonságok értékeivel.
    /// </summary>
    #endregion
    public void PlayLine()
    {
        //Noise
        CancellationTokenSource noiseTaskCancelSource = new CancellationTokenSource();
        if (NoiseFile is not null)
        {
            Task.Run(() => PlayAudioFile(NoiseFile, noiseTaskCancelSource.Token));
        }

        //TODO: Cutscene UI megjelenítése a jelenlegi sorral, meg hogy ki 

        Task.Run(() => PlayAudioFile(VoiceFile)).Wait();
        noiseTaskCancelSource.Cancel();
    }
}
