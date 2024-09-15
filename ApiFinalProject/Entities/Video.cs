namespace ApiFinalProject.Entities;

public class Video
{
    public int Id { get; set; }
    public string Name { get; set; }=string.Empty;
    public bool IsWatched { get; set; }
    public int Length { get; set; }
    public Chapter? Chapter { get; set; }
    public int ChapterID { get; set; }
    // Add file path to store the video location
    public string FilePath { get; set; } = string.Empty;
}
