using System.Text.Json.Serialization;

namespace YouTubeDataManager.Models
{
    public class YouTubeApiResponse
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; } = string.Empty;

        [JsonPropertyName("etag")]
        public string Etag { get; set; } = string.Empty;

        [JsonPropertyName("items")]
        public List<YouTubeVideoItem> Items { get; set; } = new();
    }

    public class YouTubeVideoItem
    {
        [JsonPropertyName("id")]
        public YouTubeVideoId Id { get; set; } = new();

        [JsonPropertyName("snippet")]
        public YouTubeSnippet Snippet { get; set; } = new();
    }

    public class YouTubeVideoId
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; } = string.Empty;

        [JsonPropertyName("videoId")]
        public string VideoId { get; set; } = string.Empty;

        [JsonPropertyName("channelId")]
        public string ChannelId { get; set; } = string.Empty;

        [JsonPropertyName("playlistId")]
        public string PlaylistId { get; set; } = string.Empty;
    }

    public class YouTubeSnippet
    {
        [JsonPropertyName("publishedAt")]
        public DateTime? PublishedAt { get; set; }

        [JsonPropertyName("channelId")]
        public string ChannelId { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("thumbnails")]
        public Dictionary<string, YouTubeThumbnail> Thumbnails { get; set; } = new();

        [JsonPropertyName("channelTitle")]
        public string ChannelTitle { get; set; } = string.Empty;

        [JsonPropertyName("liveBroadcastContent")]
        public string LiveBroadcastContent { get; set; } = string.Empty;
    }

    public class YouTubeThumbnail
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }
}