using ApiFinalProject.DTO.Video;
using ApiFinalProject.Entities;
using ApiFinalProject.persistence;
using Microsoft.EntityFrameworkCore;


namespace ApiFinalProject.Services.Video;

public class VideoService(ApplicationDbContext context) : IVideoService
{
    private readonly ApplicationDbContext _context = context;

    //public async Task<VideoResponseDTO?> GetVideoByIdAsync(int id)
    //{
    //    var video = await _context.Videos.FindAsync(id);
    //    if (video == null)
    //        return null;

    //    return new VideoResponseDTO
    //    {
    //        Id = video.Id,
    //        Name = video.Name,
    //        IsWatched = video.IsWatched,
    //        Length = video.Length
    //    };
    //}
    public async Task<VideoResponseDTO> GetVideoByIdAsync(int id)
    {
        var video = await _context.Videos
            .Where(v => v.Id == id)
            .Select(v => new VideoResponseDTO
            {
                Id = v.Id,
                Name = v.Name,
                IsWatched = v.IsWatched,
                Length = v.Length,
                FilePath = v.FilePath
                // VideoUrl will be set in the controller
            })
            .SingleOrDefaultAsync();

        return video;
    }


    public async Task<VideoResponseDTO> CreateVideoAsync(VideoRequestDTO videoRequest)
    {
        var video = new Entities.Video
        {
            Name = videoRequest.Name,
            Length = videoRequest.Length,
            ChapterID = videoRequest.ChapterId,
            FilePath = videoRequest.FilePath // Save file path in DB
        };

        _context.Videos.Add(video);
        await _context.SaveChangesAsync();

        return new VideoResponseDTO
        {
            Id = video.Id,
            Name = video.Name,
            Length = video.Length,
            FilePath = video.FilePath
        };
    }

    public async Task<bool> UpdateVideoAsync(int id, VideoRequestDTO videoRequest)
    {
        var video = await _context.Videos.FindAsync(id);
        if (video == null)
            return false;

        video.Name = videoRequest.Name;
        video.Length = videoRequest.Length;
        video.ChapterID = videoRequest.ChapterId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteVideoAsync(int id)
    {
        var video = await _context.Videos.FindAsync(id);
        if (video == null)
            return false;

        _context.Videos.Remove(video);
        await _context.SaveChangesAsync();
        return true;
    }
}
