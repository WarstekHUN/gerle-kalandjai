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
    #region IsFight (tulajdonság) - comment
    /// <summary>
    /// <c>IsFight</c> meghatározza, hogy van-e az adott jelenet végén harc.
    /// </summary>
    #endregion
    public bool IsFight { get; set; }
    #region Scene (paraméteres konstruktor) - comment
    /// <summary>
    /// <c>Scene</c> paraméteres konstruktor a fentebb lévő tulajdonságoknak állítja be a megfelelő értéket.
    /// </summary>
    #endregion
    public Scene(Line[] lines, bool isFight)
    {
        Lines = lines;
        IsFight = isFight;
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
