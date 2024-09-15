namespace ApiFinalProject.DTO.Video;

////public class VideoResponseDTO
////{
////    public int Id { get; set; }
////    public string Name { get; set; } = string.Empty;
////    public bool IsWatched { get; set; }
////    public int Length { get; set; }
////    public string FilePath { get; set; } = string.Empty; // Add FilePath to response
////}
public class VideoResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsWatched { get; set; }
    public int Length { get; set; }
    public string FilePath { get; set; }
    public string VideoUrl { get; set; }  // New property for video file URL
}