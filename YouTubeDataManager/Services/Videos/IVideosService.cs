using YouTubeDataManager.Models;

namespace YouTubeDataManager.Services
{
    public interface IVideosService
    {
        Task<IEnumerable<Video>> GetAll(string? title, string? author, DateTime? createdAfter, string? q);
        Task<Video> Insert(Video video);
        Task<Video?> Update(string id, Video video);
        Task<bool> Delete(string id);
        Task<int> FetchAndStoreYouTubeVideos();
    }
}