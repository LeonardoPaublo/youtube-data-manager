using Microsoft.EntityFrameworkCore;
using YouTubeDataManager.Models;

namespace YouTubeDataManager.Repositories
{
    public class VideosRepository : IVideosRepository
    {
        private readonly AppDbContext _context;

        public VideosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Video>> GetFilteredVideos(string? title, string? author, DateTime? createdAfter, string? q)
        {
            var query = _context.Videos.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(v => v.Title.Contains(title));

            if (!string.IsNullOrEmpty(author))
                query = query.Where(v => v.Author.Contains(author));

            if (createdAfter.HasValue)
                query = query.Where(v => v.PublishedAt >= createdAfter.Value);

            if (!string.IsNullOrEmpty(q))
                query = query.Where(v => v.Title.Contains(q) || v.Description.Contains(q) || v.Author.Contains(q));

            return await query.Where(v => !v.IsDeleted).ToListAsync();
        }

        public async Task<Video?> GetById(string id)
        {
            return await _context.Videos.FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted);
        }

        public async Task<Video> Insert(Video video)
        {
            _context.Videos.Add(video);
            await _context.SaveChangesAsync();
            return video;
        }

        public async Task InsertBatch(IEnumerable<Video> videos)
        {
            if (videos == null || !videos.Any())
                return;

            await _context.Videos.AddRangeAsync(videos);
            await _context.SaveChangesAsync();
        }

        public async Task<Video?> Update(Video video)
        {
            var existingVideo = await GetById(video.Id);
            if (existingVideo == null) return null;

            existingVideo.Title = video.Title;
            existingVideo.Description = video.Description;
            existingVideo.Author = video.Author;
            existingVideo.PublishedAt = video.PublishedAt;

            await _context.SaveChangesAsync();
            return existingVideo;
        }

        public async Task<bool> SoftDelete(string id)
        {
            var video = await GetById(id);
            if (video == null) return false;

            video.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}