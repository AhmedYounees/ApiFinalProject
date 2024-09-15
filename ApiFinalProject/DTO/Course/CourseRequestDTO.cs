namespace ApiFinalProject.DTO.Course;

public class CourseRequestDTO
{

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; } // Link to Category
    public string PosterImage { get; set; } = string.Empty; // Image link or Base64
}
