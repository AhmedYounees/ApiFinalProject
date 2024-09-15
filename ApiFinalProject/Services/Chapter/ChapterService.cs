using ApiFinalProject.DTO.Chapter;
using ApiFinalProject.Entities;
using ApiFinalProject.DTO.Video;
using ApiFinalProject.persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiFinalProject.Services.Chapter;

public class ChapterService(ApplicationDbContext context) : IChapterService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<ChapterResponseDTO?> GetChapterByIdAsync(int id)
    {
        var chapter = await _context.Chapters.Include(ch => ch.Videos)
            .FirstOrDefaultAsync(ch => ch.Id == id);

        if (chapter == null)
            return null;

        return new ChapterResponseDTO
        {
            Id = chapter.Id,
            Title = chapter.Title,
            Number = chapter.Number,
            Videos = chapter.Videos.Select(v => new VideoResponseDTO
            {
                Id = v.Id,
                Name = v.Name,
                IsWatched = v.IsWatched,
                Length = v.Length
            }).ToList()
        };
    }

    public async Task<ChapterResponseDTO> CreateChapterAsync(ChapterRequestDTO chapterRequest)
    {
        var chapter = new Entities.Chapter
        {
            Title = chapterRequest.Title,
            Number = chapterRequest.Number,
            CourseId = chapterRequest.CourseId
        };

        _context.Chapters.Add(chapter);
        await _context.SaveChangesAsync();

        return new ChapterResponseDTO
        {
            Id = chapter.Id,
            Title = chapter.Title,
            Number = chapter.Number
        };
    }

    public async Task<bool> UpdateChapterAsync(int id, ChapterRequestDTO chapterRequest)
    {
        var chapter = await _context.Chapters.FindAsync(id);
        if (chapter == null)
            return false;

        chapter.Title = chapterRequest.Title;
        chapter.Number = chapterRequest.Number;

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteChapterAsync(int id)
    {
        var chapter = await _context.Chapters.FindAsync(id);
        if (chapter == null)
            return false;

        _context.Chapters.Remove(chapter);
        await _context.SaveChangesAsync();
        return true;
    }


}
