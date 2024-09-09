namespace ApiFinalProject.Entities;

public class Category
{

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Course> Courses { get; set; } = new List<Course>();

    public Specialization Specialization { get; set; } = null!;
    public int? SpecializationId { get; set; }
}
