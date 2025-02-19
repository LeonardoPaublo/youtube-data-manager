using YouTubeDataManager.Models;

namespace YouTubeDataManager.Services
{
    public interface IYouTubeApiService
    {
        Task<IEnumerable<Video>> GetVideosAsync();
    }
}
