using ApiFinalProject.DTO.Category;
using ApiFinalProject.DTO.Chapter;

namespace ApiFinalProject.DTO.Course;

public class CourseResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PosterImage { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }

    public CategoryDTO Category { get; set; } = new CategoryDTO();
    public ICollection<ChapterResponseDTO> Chapters { get; set; } = new List<ChapterResponseDTO>();
}
