using System.ComponentModel.DataAnnotations;

namespace ApiFinalProject.DTO.Video;

public class VideoUploadDTO
{
    public string Name { get; set; } = string.Empty;
    public int Length { get; set; }
    public int ChapterId { get; set; }

    [Required]
    public IFormFile VideoFile { get; set; } = default!;
}
