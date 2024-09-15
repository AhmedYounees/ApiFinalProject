using ApiFinalProject.DTO.Chapter;

namespace ApiFinalProject.Services.Chapter;

public interface IChapterService
{
    Task<ChapterResponseDTO?> GetChapterByIdAsync(int id);
    Task<ChapterResponseDTO> CreateChapterAsync(ChapterRequestDTO chapterRequest);
    Task<bool> UpdateChapterAsync(int id, ChapterRequestDTO chapterRequest);
    Task<bool> DeleteChapterAsync(int id);
}
