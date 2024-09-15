using ApiFinalProject.DTO.Category;
using ApiFinalProject.DTO.Chapter;
using ApiFinalProject.DTO.Course;
using ApiFinalProject.DTO.Video;
using ApiFinalProject.persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiFinalProject.Services.Course;

public class CourseService(ApplicationDbContext context) : ICourseService
{
    private readonly ApplicationDbContext _context = context;


    public async Task<CourseResponseDTO?> GetCourseByIdAsync(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Category)
            .Include(c => c.Chapters)
            .ThenInclude(ch => ch.Videos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
            return null;

        return new CourseResponseDTO
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            Price = course.Price,
            CreatedOn = course.CreatedOn,
            UpdatedOn = course.UpdatedOn,
            PosterImage = course.PosterImage,
            Category = new CategoryDTO { Id = course.Category.Id, Name = course.Category.Name },
            Chapters = course.Chapters.Select(ch => new ChapterResponseDTO
            {
                Id = ch.Id,
                Title = ch.Title,
                Number = ch.Number,
                Videos = ch.Videos.Select(v => new VideoResponseDTO
                {
                    Id = v.Id,
                    Name = v.Name,
                    IsWatched = v.IsWatched,
                    Length = v.Length
                }).ToList()
            }).ToList()
        };
    }


    public async Task<CourseResponseDTO> CreateCourseAsync(CourseRequestDTO courseRequest)
    {
        var course = new Entities.Course
        {
            Name = courseRequest.Name,
            Description = courseRequest.Description,
            Price = courseRequest.Price,
            CategoryId = courseRequest.CategoryId,
            PosterImage = courseRequest.PosterImage
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return new CourseResponseDTO
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            Price = course.Price,
            CreatedOn = course.CreatedOn,
            PosterImage = course.PosterImage
        };
    }

    public async Task<bool> UpdateCourseAsync(int id, CourseRequestDTO courseRequest)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            return false;

        course.Name = courseRequest.Name;
        course.Description = courseRequest.Description;
        course.Price = courseRequest.Price;
        course.CategoryId = courseRequest.CategoryId;
        course.PosterImage = courseRequest.PosterImage;
        course.UpdatedOn = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteCourseAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            return false;

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return true;
    }
}
