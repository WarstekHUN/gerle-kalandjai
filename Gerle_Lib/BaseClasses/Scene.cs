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
public unsafe class Scene
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
    public Actor Opponent { get; init; }

    /// <summary>
    /// Megadja hogy az adott jelenet melyik történetágon helyezkedik el. A játék végén van jelentősége
    /// </summary>
    public SceneVersion Version { get; init; }

    #region Scene (paraméteres konstruktor) - comment
    /// <summary>
    /// <c>Scene</c> paraméteres konstruktor a fentebb lévő tulajdonságoknak állítja be a megfelelő értéket.
    /// </summary>
    #endregion
    public Scene(Line[] lines, Actor opponent, SceneVersion choiceVersion = SceneVersion.BASE)
    {
        Lines = lines;
        Opponent = opponent;
        Version = choiceVersion;
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
