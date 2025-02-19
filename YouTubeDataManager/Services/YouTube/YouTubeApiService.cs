using System.Net.Http;
using System.Text.Json;
using System.Web;
using YouTubeDataManager.Models;

namespace YouTubeDataManager.Services
{
    public class YouTubeApiService : IYouTubeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string YouTubeApiUrl = "https://www.googleapis.com/youtube/v3/search";

        public YouTubeApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("YouTubeApiKey") 
                      ?? configuration["YouTubeApiKey"] 
                      ?? throw new ArgumentNullException("YouTube API key not found.");
        }

        public async Task<IEnumerable<Video>> GetVideosAsync()
        {
            string query = "medication manipulation";
            string regionCode = "BR";
            string publishedAfter = "2025-01-01T00:00:00Z";

            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            queryParams["part"] = "snippet";
            queryParams["q"] = query;
            queryParams["regionCode"] = regionCode;
            queryParams["publishedAfter"] = publishedAfter;
            queryParams["type"] = "video";
            queryParams["key"] = _apiKey;

            string requestUrl = $"{YouTubeApiUrl}?{queryParams}";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch videos: {response.ReasonPhrase}");
            }

            string json = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonSerializer.Deserialize<YouTubeApiResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return parsedResponse?.Items?.Select(item => new Video
            {
                Title = item.Snippet.Title,
                Author = item.Snippet.ChannelTitle,
                PublishedAt = item.Snippet.PublishedAt ?? DateTime.MinValue,
                Id = item.Id.VideoId,
                Description = item.Snippet.Description,
            }) ?? new List<Video>();
        }
    }
}