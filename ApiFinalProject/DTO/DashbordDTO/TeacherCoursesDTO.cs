using ApiFinalProject.Entities;

namespace ApiFinalProject.DTO.DashbordDTO
{
    public class TeacherCoursesDTO
    {
        public string TeaherName { get; set; }
        public ICollection<CourseDashbordDTO>courses { get; set; }
    }
}
