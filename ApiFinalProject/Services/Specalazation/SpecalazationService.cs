using ApiFinalProject.Entities;
using ApiFinalProject.persistence;

namespace ApiFinalProject.Services.Specalazation
{
    public class SpecalazationService :ISpecalazation
    {
        ApplicationDbContext context;

        public SpecalazationService(ApplicationDbContext con)
        {
            context = con;

        }
        public void add(Specialization s)
        {
            context.Add(s);
        }
        public void update(Specialization s)
        {
            context.Update(s);
        }
        public void delete(Specialization s)
        {
            context.Remove(s);
        }
        public List<Specialization> GetAll()
        {
            return context.Specializations.ToList();
        }
        public Specialization GetbyID(int id)
        {
            return context.Specializations.FirstOrDefault(d => d.Id == id);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
