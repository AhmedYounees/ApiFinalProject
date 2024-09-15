using ApiFinalProject.DTO.Video;

namespace ApiFinalProject.DTO.Chapter;

public class ChapterResponseDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public byte Number { get; set; }

    public ICollection<VideoResponseDTO> Videos { get; set; } = new List<VideoResponseDTO>();
}