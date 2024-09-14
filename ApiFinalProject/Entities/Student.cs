using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFinalProject.Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Grad { get; set; }
    public ICollection<CourseStudent> StudentCourses { get; set; } = new List<CourseStudent>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    [ForeignKey("ApplicationUser")]
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

}
