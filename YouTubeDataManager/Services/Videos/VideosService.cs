using YouTubeDataManager.Models;
using YouTubeDataManager.Repositories;

namespace YouTubeDataManager.Services
{
    public class VideosService : IVideosService
    {
        private readonly IVideosRepository _videosRepository;
        private readonly IYouTubeApiService _youTubeApiService;

        public VideosService(IVideosRepository videosRepository, IYouTubeApiService youTubeApiService)
        {
            _videosRepository = videosRepository;
            _youTubeApiService = youTubeApiService;
        }

        public async Task<IEnumerable<Video>> GetAll(string? title, string? author, DateTime? createdAfter, string? q)
        {
            return await _videosRepository.GetFilteredVideos(title, author, createdAfter, q);
        }

        public async Task<Video> Insert(Video video)
        {
            return await _videosRepository.Insert(video);
        }

        public async Task<Video?> Update(string id, Video video)
        {
            var existingVideo = await _videosRepository.GetById(id);
            if (existingVideo == null) return null;

            existingVideo.Title = video.Title;
            existingVideo.Description = video.Description;
            existingVideo.Author = video.Author;
            existingVideo.PublishedAt = video.PublishedAt;

            return await _videosRepository.Update(existingVideo);
        }

        public async Task<bool> Delete(string id)
        {
            return await _videosRepository.SoftDelete(id);
        }

        public async Task<int> FetchAndStoreYouTubeVideos()
        {
            var youtubeVideos = await _youTubeApiService.GetVideosAsync();

            if (youtubeVideos == null || !youtubeVideos.Any())
            {
                return 0;
            }

            var newVideos = youtubeVideos
                .Select(video => new Video
                {
                    Title = video.Title,
                    Description = video.Description,
                    Author = video.Author,
                    PublishedAt = video.PublishedAt,
                    Id = video.Id,
                    IsDeleted = false
                })
                .ToList();

            await _videosRepository.InsertBatch(newVideos);

            return newVideos.Count;
        }
    }
}