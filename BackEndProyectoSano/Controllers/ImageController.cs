using BackEndProyectoSano.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProyectoSano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile image, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No se proporcionó ninguna imagen o la imagen estaba vacía.");
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var uploadAzure = new UploadAzure();

            using (var stream = image.OpenReadStream())
            {
                await uploadAzure.UploadBlobAsync("product1", uniqueFileName, stream);
            }

            var imageUrl = "https://imagetesteo1.blob.core.windows.net/product1/" + uniqueFileName;
            return Ok(new { imageUrl });
        }
    }
}
