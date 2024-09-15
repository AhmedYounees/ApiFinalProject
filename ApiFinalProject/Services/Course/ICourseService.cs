using ApiFinalProject.DTO.Course;

namespace ApiFinalProject.Services.Course;

public interface ICourseService
{
    Task<CourseResponseDTO?> GetCourseByIdAsync(int id);
    Task<CourseResponseDTO> CreateCourseAsync(CourseRequestDTO courseRequest);
    Task<bool> UpdateCourseAsync(int id, CourseRequestDTO courseRequest);
    Task<bool> DeleteCourseAsync(int id);
}
