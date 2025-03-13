using BackEndProyectoSano.DBaseContext;
using BackEndProyectoSano.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProyectoSano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VideoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideos()
        {
            return await _context.Videos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            return video;
        }

        [HttpPost]
        public async Task<ActionResult<Video>> CreateVideo(Video video)
        {
            var videoId = ExtractVideoId(video.Url);
            if (string.IsNullOrEmpty(videoId))
            {
                return BadRequest("La URL del video no es válida.");
            }

            video.Image = $"https://img.youtube.com/vi/{videoId}/hqdefault.jpg";

            _context.Videos.Add(video);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVideo), new { id = video.Id }, video);
        }

        private string ExtractVideoId(string url)
        {
            var uri = new Uri(url);
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

            if (query["v"] != null)
            {
                return query["v"];
            }

            var segments = uri.Segments;
            if (segments.Length > 1 && segments[1].Length == 11)
            {
                return segments[1].Trim('/');
            }

            return null;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideo(int id, Video video)
        {
            if (id != video.Id)
            {
                return BadRequest();
            }

            _context.Entry(video).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VideoExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }
    }
}
