using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using YouTubeDataManager.Models;
using YouTubeDataManager.Services;

namespace YouTubeDataManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IVideosService _videosService;

        public VideosController(IVideosService videosService)
        {
            _videosService = videosService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? title,
            [FromQuery] string? author,
            [FromQuery] DateTime? createdAfter,
            [FromQuery] string? q)
        {
            IEnumerable<Video> videos = await _videosService.GetAll(title, author, createdAfter, q);
            return Ok(JsonSerializer.Serialize(videos));
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Video video)
        {
            Video newVideo = await _videosService.Insert(video);
            return CreatedAtAction(nameof(GetAll), new { id = newVideo.Id }, newVideo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Video video)
        {
            Video? updatedVideo = await _videosService.Update(id, video);
            if (updatedVideo == null)
                return NotFound(new { message = "Video not found." });

            return Ok(updatedVideo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool success = await _videosService.Delete(id);
            if (!success)
                return NotFound(new { message = "Video not found or already deleted." });

            return NoContent();
        }

        [HttpPost("fetch")]
        public async Task<IActionResult> FetchVideos()
        {
            int fetchedVideos = await _videosService.FetchAndStoreYouTubeVideos();
            return Ok(fetchedVideos);
        }
    }
}