public class Story
{
    public Line[] Lines { get; set; }
    public bool IsFight { get; set; }

    public Story(Line[] lines, bool isFight)
    {
        Lines = lines;
        IsFight = isFight;
    }
}
