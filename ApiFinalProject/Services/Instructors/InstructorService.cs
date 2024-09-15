using ApiFinalProject.Entities;
using ApiFinalProject.persistence;

namespace ApiFinalProject.Services.Instructors
{
    public class InstructorService : IInstructor
    {
        ApplicationDbContext context;

        public InstructorService(ApplicationDbContext con)
        {
            context = con;

        }
        public void add(Instructor i)
        {
            context.Add(i);
        }
        public void update(Instructor i)
        {
            context.Update(i);
        }
        public void delete(Instructor i)
        {
            context.Remove(i);
        }
        public List<Instructor> GetAll()
        {
            return context.Instructors.ToList();
        }
        public Instructor GetbyID(int id)
        {
            return context.Instructors.FirstOrDefault(d => d.Id == id);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
