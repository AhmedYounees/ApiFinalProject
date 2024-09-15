using ApiFinalProject.DTO.Video;
using ApiFinalProject.Services.Video;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinalProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VideosController(IVideoService videoService, IWebHostEnvironment environment) : ControllerBase
{
    private readonly IVideoService _videoService = videoService;
    private readonly IWebHostEnvironment _env = environment;

    [HttpGet("{id}")]
    public async Task<ActionResult<VideoResponseDTO>> GetVideo(int id)
    {
        var video = await _videoService.GetVideoByIdAsync(id); 
        if (video == null)
            return NotFound();

        return Ok(video);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadVideo([FromForm] VideoUploadDTO videoUploadDTO)
    {
        if (videoUploadDTO.VideoFile == null || videoUploadDTO.VideoFile.Length == 0)
        {
            return BadRequest("Video file is required.");
        }

        // Save video to the server
        var videoFileName = $"{Guid.NewGuid()}_{videoUploadDTO.VideoFile.FileName}";
        var filePath = Path.Combine(_env.WebRootPath, "videos", videoFileName);

        // Ensure the directory exists
        if (!Directory.Exists(Path.Combine(_env.WebRootPath, "videos")))
        {
            Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "videos"));
        }

        // Copy file to the server location
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await videoUploadDTO.VideoFile.CopyToAsync(stream);
        }

        // Save video metadata and file path to database
        var videoResponse = await _videoService.CreateVideoAsync(new VideoRequestDTO
        {
            Name = videoUploadDTO.Name,
            Length = videoUploadDTO.Length,
            ChapterId = videoUploadDTO.ChapterId,
            FilePath = Path.Combine("videos", videoFileName) // Store relative path
        });

        return CreatedAtAction(nameof(GetVideo), new { id = videoResponse.Id }, videoResponse);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateVideo(int id, [FromBody] VideoRequestDTO videoRequest)
    {
        var updated = await _videoService.UpdateVideoAsync(id, videoRequest);
        if (!updated)
            return NotFound();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteVideo(int id)
    {
        var deleted = await _videoService.DeleteVideoAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

}
