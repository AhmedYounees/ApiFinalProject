using ApiFinalProject.DTO.Course;
using ApiFinalProject.Services.Course;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinalProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoursesController(ICourseService courseService , IWebHostEnvironment webHostEnvironment) : ControllerBase
{
    private readonly ICourseService _courseService = courseService;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseResponseDTO>> GetCourse(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
            return NotFound();

        return Ok(course);
    }

    [HttpPost]
    public async Task<ActionResult<CourseResponseDTO>> CreateCourse([FromBody] CourseRequestDTO courseRequest)
    {
        var course = await _courseService.CreateCourseAsync(courseRequest);
        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(int id, [FromBody] CourseRequestDTO courseRequest)
    {
        var updated = await _courseService.UpdateCourseAsync(id, courseRequest);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {
        var deleted = await _courseService.DeleteCourseAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }


    [HttpPost]
    [Route("upload-poster")]
    public async Task<IActionResult> UploadPoster([FromForm] IFormFile posterImage)
    {
        if (posterImage == null || posterImage.Length == 0)
        {
            return BadRequest("No file uploaded or file is empty.");
        }

        // Define the directory where files will be saved
        var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

        // Ensure the directory exists
        if (!Directory.Exists(uploadsFolderPath))
        {
            Directory.CreateDirectory(uploadsFolderPath);
        }

        // Generate a unique filename for the image
        var uniqueFileName = Guid.NewGuid().ToString() + "_" + posterImage.FileName;

        // Full path to save the file
        var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

        // Save the file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await posterImage.CopyToAsync(stream);
        }

        // Return the path or the filename that was saved
        return Ok(new { filePath = $"uploads/{uniqueFileName}" });
    }
}
