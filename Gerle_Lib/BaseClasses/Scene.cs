using Gerle_Lib.BaseClasses;

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
    public SceneMusic FightMusic { get; init; }

    /// <summary>
    /// Olyan jelenetet hoz létre, aminek a végén nincsen harc
    /// </summary>
    /// <param name="lines">Karakterek által elmondott szövegek</param>
    /// <param name="choiceVersion">Megadja, hogy az adott jelenet melyik történetszálhoz tartozzon</param>
    public Scene(Line[] lines, SceneVersion choiceVersion = SceneVersion.BASE)
    {
        Lines = lines;
        Version = choiceVersion;
    }

    /// <summary>
    /// Olyan jelenetet hoz létre, aminek a végén van harc
    /// </summary>
    /// <param name="lines">Karakterek által elmondott szövegek</param>
    /// <param name="opponent">A jelenet ellensége</param>
    /// <param name="fightMusic">A jelenet harci zenéje</param>
    /// <param name="choiceVersion">Megadja, hogy az adott jelenet melyik történetszálhoz tartozzon</param>
    public Scene(Line[] lines, ref Actor opponent, SceneMusic fightMusic, SceneVersion choiceVersion = SceneVersion.BASE)
    {
        Lines = lines;
        Opponent = opponent;
        Version = choiceVersion;
        FightMusic = fightMusic;
    }

    #region PlayScene (metódus) - comment
    /// <summary>
    /// <c>PlayScene</c> metódus felel az adott jelenet lejátszásáért.
    /// </summary>
    #endregion
    public void PlayScene()
    {
        foreach (var line in Lines)
        {
            line.PlayLine();
        }
    }
}
