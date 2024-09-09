namespace ApiFinalProject.Entities;

public class Instructor
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public byte ExperienceAge { get; set; }

    public Specialization? Specialization { get; set; }
    public int SpecializationId { get; set; }
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<CourseInstructor> InstructorCourses { get; set; } = new List<CourseInstructor>();

}
