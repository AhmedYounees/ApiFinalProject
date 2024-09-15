namespace ApiFinalProject.DTO.Video;

public class VideoRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public int Length { get; set; } // Length of the video in seconds
    public int ChapterId { get; set; }
    public string FilePath { get; set; } = string.Empty; // Add FilePath to request

}
