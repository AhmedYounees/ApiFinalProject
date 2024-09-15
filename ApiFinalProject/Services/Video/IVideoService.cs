using ApiFinalProject.DTO.Video;

namespace ApiFinalProject.Services.Video;

public interface IVideoService
{
    Task<VideoResponseDTO?> GetVideoByIdAsync(int id);
    Task<VideoResponseDTO> CreateVideoAsync(VideoRequestDTO videoRequest);
    Task<bool> UpdateVideoAsync(int id, VideoRequestDTO videoRequest);
    Task<bool> DeleteVideoAsync(int id);
}
