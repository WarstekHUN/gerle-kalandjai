public class Scene
{
    public Line[] Lines { get; set; }
    public bool IsFight { get; set; }

    public Scene(Line[] lines, bool isFight)
    {
        Lines = lines;
        IsFight = isFight;
    }

    public void PlayScene()
    {
        throw new NotImplementedException();
    }
}
