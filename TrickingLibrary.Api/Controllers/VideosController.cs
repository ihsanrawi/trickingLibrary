using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace TrickingLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public VideosController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("{video}")]
        public IActionResult GetVideo(string video)
        {
            var mime = video.Split('.').Last();
            var savePath = Path.Combine(_env.WebRootPath, video);
            return new FileStreamResult(new FileStream(savePath, FileMode.Open, FileAccess.Read), "video/*");
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideo(IFormFile video)
        {
            var mime = video.FileName.Split('.').Last();
            var fileName = string.Concat(Path.GetRandomFileName(), ".", mime);
            var savePath = Path.Combine(_env.WebRootPath, fileName);

            await using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                await video.CopyToAsync(fileStream);
            }

            return Ok(fileName);
        }
    }
