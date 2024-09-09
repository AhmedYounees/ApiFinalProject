namespace ApiFinalProject.Entities;

public class Specialization
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public ICollection<Instructor> Instructors { get; set; } = [];

    public ICollection<Category> Categories { get; set; } = [];

}
