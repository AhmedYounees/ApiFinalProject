using ApiFinalProject.Entities;

namespace ApiFinalProject.Services.Specalazation
{
    public interface ISpecalazation
    {
        public void add(Specialization s);
        public void update(Specialization s);
        public void delete(Specialization s);
        public List<Specialization> GetAll();
        public Specialization GetbyID(int id);
        public void Save();
    }
}
