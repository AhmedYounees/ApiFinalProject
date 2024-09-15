using ApiFinalProject.Entities;

namespace ApiFinalProject.Services.Instructors
{
    public interface IInstructor
    {
        public void add(Instructor i);
        public void update(Instructor i);
        public void delete(Instructor i);
        public List<Instructor> GetAll();
        public Instructor GetbyID(int id);
        public void Save();
    }
}
