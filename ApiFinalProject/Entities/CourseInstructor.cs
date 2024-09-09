namespace ApiFinalProject.Entities;

public class CourseInstructor
{
    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; } = default!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = default!;
}
