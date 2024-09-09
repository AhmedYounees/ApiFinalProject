namespace ApiFinalProject.Entities;

public class Chapter
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public byte Number { get; set; }

    public ICollection<Video> Videos { get; set; } = [];

    public Course Course { get; set; } = default!;
    public int CourseId { get; set; }
}
