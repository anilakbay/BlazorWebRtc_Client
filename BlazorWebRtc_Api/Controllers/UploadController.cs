using BlazorWebRtc_Application.Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadController(IUploadService uploadService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _uploadService = uploadService;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, string userId)
        {

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Dosyanın wwwroot altında saklanacağı dizin
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            // Eğer dizin yoksa oluşturulur
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Benzersiz bir dosya adı oluşturulur
            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadPath, fileName);

            // Dosya sistemine kaydetme işlemi
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Dosyanın tarayıcıda erişilebilir yolu
            var imageUrl = $"/images/{fileName}";

            return Ok(imageUrl);  // Bu URL frontend'e dönecek
        }
    }
}
