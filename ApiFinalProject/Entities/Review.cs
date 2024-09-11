using System.ComponentModel.DataAnnotations;

namespace ApiFinalProject.Entities;

public class Review
{

    public int Id { get; set; }
    [Range(0, 5)]
    public int? Rate { get; set; } = default!;
    public string Comment { get; set; } = string.Empty;
    public DateTime? ReviewDate { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

}
