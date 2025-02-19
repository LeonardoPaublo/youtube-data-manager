using YouTubeDataManager.Models;

namespace YouTubeDataManager.Repositories
{
    public interface IVideosRepository
    {
        Task<IEnumerable<Video>> GetFilteredVideos(string? title, string? author, DateTime? createdAfter, string? q);
        Task<Video?> GetById(string id);
        Task<Video> Insert(Video video);
        Task InsertBatch(IEnumerable<Video> videos);
        Task<Video?> Update(Video video);
        Task<bool> SoftDelete(string id);
    }
}