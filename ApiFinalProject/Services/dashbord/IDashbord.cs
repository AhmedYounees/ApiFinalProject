using ApiFinalProject.DTO.DashbordDTO;

namespace ApiFinalProject.Services.dashbord
{
    public interface IDashbord
    {
        public Task<IEnumerable<TeacherCoursesDTO>> GetAllTeacherWithCourses();
    }
}
