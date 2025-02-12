﻿using Gerle_Lib.BaseClasses;
using Gerle_Lib.Controllers;
using Gerle_Lib.UIReleated;
using NAudio.Wave;

public enum SceneVersion
{
    BASE,
    A,
    B
}

#region Scene (osztály) - comment
/// <summary>
/// <c>Scene</c> osztály a jeleneteket tartalmazz, kezeli (<c>Scene.cs</c>).
/// </summary>
#endregion
public class Scene
{
    #region Lines (tulajdonság) - comment
    /// <summary>
    /// <c>Lines</c> tulajdonság a szereplők által elmondott sorokat tartalmazza (<c>Line.cs</c>).
    /// </summary>
    #endregion
    public Line[] Lines { get; set; }
    #region Opponent (tulajdonság) - comment
    /// <summary>
    /// <c>Opponent</c> Amennyiben nem null, a jelenet után harc kezdődik az adott karakterrel.
    /// </summary>
    #endregion
    public Actor? Opponent { get; init; }

    #region Version (tulajdonság)
    /// <summary>
    /// Megadja hogy az adott jelenet melyik történetágon helyezkedik el. A játék végén van jelentősége
    /// </summary>
    #endregion
    public SceneVersion Version { get; init; }

    /// <summary>
    /// Tartalmazza a jelenet harci zenéjét.
    /// </summary>
    public SceneMusic? FightMusic { get; init; }

    /// <summary>
    /// Tartalmazza a scene alatt lejátszandó háttérzaj fájlnevét (kiterjesztés és elérési út nélkül)
    /// </summary>
    public string? NoiseFile { get; init; }

    /// <summary>
    /// Olyan jelenetet hoz létre, aminek a végén nincsen harc
    /// </summary>
    /// <param name="lines">Karakterek által elmondott szövegek</param>
    /// <param name="noiseFile">A scene alatt lejátszandó háttérzaj fájlneve (kiterjesztés és elérési út nélkül)</param>
    /// <param name="choiceVersion">Megadja, hogy az adott jelenet melyik történetszálhoz tartozzon</param>
    public Scene(Line[] lines, string? noiseFile = null, SceneVersion choiceVersion = SceneVersion.BASE)
    {
        Lines = lines;
        Version = choiceVersion;
        NoiseFile = noiseFile;
    }

    /// <summary>
    /// Olyan jelenetet hoz létre, aminek a végén van harc
    /// </summary>
    /// <param name="lines">Karakterek által elmondott szövegek</param>
    /// <param name="opponent">A jelenet ellensége</param>
    /// <param name="fightMusic">A jelenet harci zenéje</param>
    /// <param name="noiseFile">A scene alatt lejátszandó háttérzaj fájlneve (kiterjesztés és elérési út nélkül)</param>
    /// <param name="choiceVersion">Megadja, hogy az adott jelenet melyik történetszálhoz tartozzon</param>
    public Scene(Line[] lines, ref Actor opponent, SceneMusic fightMusic, string? noiseFile = null, SceneVersion choiceVersion = SceneVersion.BASE)
    {
        Lines = lines;
        Opponent = opponent;
        Version = choiceVersion;
        FightMusic = fightMusic;
        NoiseFile = noiseFile;
    }

    #region PlayScene (metódus) - comment
    /// <summary>
    /// <c>PlayScene</c> metódus felel az adott jelenet lejátszásáért.
    /// </summary>
    #endregion
    public virtual async Task<SceneVersion> PlayScene()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        Task noisePlayer = Task.Run(async () =>
        {
            TaskCompletionSource completionSource = new TaskCompletionSource();

            if (NoiseFile is null) completionSource.SetResult();

            Mp3FileReader noiseReader = new Mp3FileReader(NoiseFile);
            
                WaveOutEvent player = new WaveOutEvent();
                player.Init(noiseReader);
                player.Volume = SettingsController.MasterVolume * SettingsController.FXVolume;
                player.Play();

                //TODO: Ide lehetne rakni egy fade out-ot, de nem szükséges
                cts.Token.Register(() => player.Stop());

                player.PlaybackStopped += (object? sender, StoppedEventArgs e) =>
                {
                    if (e.Exception is not null) throw e.Exception;
                    noiseReader.Dispose();
                    completionSource.SetResult();
                };
            

            await completionSource.Task;
        });

        SceneVersion version = await UI.CutsceneUI(Lines);

        cts.Cancel();
        return version;
    }
}
