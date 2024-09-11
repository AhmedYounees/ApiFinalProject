namespace ApiFinalProject.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public string PosterImage {  get; set; } = string.Empty;
    public DateTime? UpdatedOn { get; set; }
    public decimal Price { get; set; }
    //relations
    public Category Category { get; set; } = default!;
    public int CategoryId { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Chapter> Chapters { get; set; } = [];
    public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    // public ICollection<Student> Students { get; set; }=new List<Student>();
    public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();
    public ICollection<CourseStudent> StudentCourses { get; set; } = new List<CourseStudent>();

}
