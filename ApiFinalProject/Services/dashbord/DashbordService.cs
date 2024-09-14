using ApiFinalProject.DTO.DashbordDTO;
using ApiFinalProject.Entities;
using ApiFinalProject.persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiFinalProject.Services.dashbord
{
    public class DashbordService(ApplicationDbContext _context) : IDashbord
    {
        private readonly ApplicationDbContext context = _context;

        public async Task<IEnumerable<TeacherCoursesDTO>> GetAllTeacherWithCourses()
        {
            var list = await context.Instructors.Include(ins => ins.InstructorCourses).ThenInclude(insc => insc.Course).Select(Tc => new TeacherCoursesDTO
            {
                TeaherName = Tc.Name,
                courses = Tc.InstructorCourses.Select(tc => new CourseDashbordDTO
                {
                    Id=tc.Course.Id,
                    Name = tc.Course.Name,
                    price=tc.Course.Price,

                }).ToList()
            }).ToListAsync();
            return list;    
        }
    }
}
