using ApiFinalProject.DTO.Video;
using ApiFinalProject.Services.Video;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;

namespace ApiFinalProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VideosController(IVideoService videoService, IWebHostEnvironment environment, ILogger<VideosController> logger) : ControllerBase
{
    private readonly IVideoService _videoService = videoService;
    private readonly IWebHostEnvironment _env = environment;
    private readonly ILogger<VideosController> _logger=logger;

    //[HttpGet("{id}")]
    //public async Task<ActionResult<VideoResponseDTO>> GetVideo(int id)
    //{
    //    var video = await _videoService.GetVideoByIdAsync(id); 
    //    if (video == null)
    //        return NotFound();

    //    return Ok(video);
    //}
    



    [HttpGet("{id}")]
    public async Task<ActionResult<VideoResponseDTO>> GetVideo(int id)
    {
        var video = await _videoService.GetVideoByIdAsync(id);
        if (video == null)
        {
            _logger.LogWarning($"Video with ID {id} not found.");
            return NotFound();
        }

        // Set the VideoUrl
        video.VideoUrl = Url.Action("GetVideoFile", "Videos", new { id = video.Id }, Request.Scheme);

        return Ok(video);
    }

    [HttpGet("{id}/file")]
    public async Task<IActionResult> GetVideoFile(int id)
    {
        var video = await _videoService.GetVideoByIdAsync(id);
        if (video == null)
        {
            _logger.LogWarning($"Video with ID {id} not found.");
            return NotFound();
        }

        var path = Path.Combine(_env.WebRootPath, video.FilePath);
        if (!System.IO.File.Exists(path))
        {
            _logger.LogWarning($"Video file not found for ID {id}. Path: {path}");
            return NotFound("Video file not found.");
        }

        _logger.LogInformation($"Serving video file for ID {id}. Path: {path}");
        return PhysicalFile(path, "application/octet-stream", enableRangeProcessing: true);
    }

    // ... other methods (UploadVideo, UpdateVideo, DeleteVideo) ...

//[HttpPost("upload")]
//public async Task<IActionResult> UploadVideo([FromForm] VideoUploadDTO videoUploadDTO)
//{
//    if (videoUploadDTO.VideoFile == null || videoUploadDTO.VideoFile.Length == 0)
//    {
//        return BadRequest("Video file is required.");
//    }

//    // Save video to the server
//    var videoFileName = $"{Guid.NewGuid()}_{videoUploadDTO.VideoFile.FileName}";
//    var filePath = Path.Combine(_env.WebRootPath, "videos", videoFileName);

//    // Ensure the directory exists
//    if (!Directory.Exists(Path.Combine(_env.WebRootPath, "videos")))
//    {
//        Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "videos"));
//    }

//    // Copy file to the server location
//    using (var stream = new FileStream(filePath, FileMode.Create))
//    {
//        await videoUploadDTO.VideoFile.CopyToAsync(stream);
//    }

//    // Save video metadata and file path to database
//    var videoResponse = await _videoService.CreateVideoAsync(new VideoRequestDTO
//    {
//        Name = videoUploadDTO.Name,
//        Length = videoUploadDTO.Length,
//        ChapterId = videoUploadDTO.ChapterId,
//        FilePath = Path.Combine("videos", videoFileName) // Store relative path
//    });

//    return CreatedAtAction(nameof(GetVideo), new { id = videoResponse.Id }, videoResponse);
//}


//it work will cludeia
//[HttpPost("upload")]
//public async Task<IActionResult> UploadVideo([FromForm] VideoUploadDTO videoUploadDTO)
//{
//    if (videoUploadDTO.VideoFile == null || videoUploadDTO.VideoFile.Length == 0)
//    {
//        return BadRequest("Video file is required.");
//    }

//    // Determine the root path for uploads
//    string uploadPath = _env.WebRootPath;
//    if (string.IsNullOrEmpty(uploadPath))
//    {
//        uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
//    }

//    // Create the videos directory if it doesn't exist
//    string videosDirectory = Path.Combine(uploadPath, "videos");
//    Directory.CreateDirectory(videosDirectory);

//    // Generate a unique filename
//    var videoFileName = $"{Guid.NewGuid()}_{videoUploadDTO.VideoFile.FileName}";
//    var filePath = Path.Combine(videosDirectory, videoFileName);

//    // Copy file to the server location
//    using (var stream = new FileStream(filePath, FileMode.Create))
//    {
//        await videoUploadDTO.VideoFile.CopyToAsync(stream);
//    }

//    // Save video metadata and file path to database
//    var videoResponse = await _videoService.CreateVideoAsync(new VideoRequestDTO
//    {
//        Name = videoUploadDTO.Name,
//        Length = videoUploadDTO.Length,
//        ChapterId = videoUploadDTO.ChapterId,
//        FilePath = Path.Combine("videos", videoFileName) // Store relative path
//    });

//    return CreatedAtAction(nameof(GetVideo), new { id = videoResponse.Id }, videoResponse);
//}
[HttpPost("upload")]
    public async Task<IActionResult> UploadVideo([FromForm] VideoUploadDTO videoUploadDTO)
    {
        try
        {
            if (videoUploadDTO.VideoFile == null || videoUploadDTO.VideoFile.Length == 0)
            {
                return BadRequest("Video file is required.");
            }

            // Determine the root path for uploads
            string uploadPath = _env.WebRootPath;
            if (string.IsNullOrEmpty(uploadPath))
            {
                uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            _logger.LogInformation($"Upload path: {uploadPath}");

            // Create the videos directory if it doesn't exist
            string videosDirectory = Path.Combine(uploadPath, "videos");
            Directory.CreateDirectory(videosDirectory);
            _logger.LogInformation($"Videos directory: {videosDirectory}");

            // Generate a unique filename
            var videoFileName = $"{Guid.NewGuid()}_{videoUploadDTO.VideoFile.FileName}";
            var filePath = Path.Combine(videosDirectory, videoFileName);
            _logger.LogInformation($"File path: {filePath}");

            // Copy file to the server location
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await videoUploadDTO.VideoFile.CopyToAsync(stream);
            }
            _logger.LogInformation("File copied successfully");

            // Save video metadata and file path to database
            var videoResponse = await _videoService.CreateVideoAsync(new VideoRequestDTO
            {
                Name = videoUploadDTO.Name,
                Length = videoUploadDTO.Length,
                ChapterId = videoUploadDTO.ChapterId,
                FilePath = Path.Combine("videos", videoFileName) // Store relative path
            });
            _logger.LogInformation($"Video created with ID: {videoResponse.Id}");

            return CreatedAtAction(nameof(GetVideo), new { id = videoResponse.Id }, videoResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while uploading video");
            return StatusCode(500, "An error occurred while processing your request.");
        }
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
