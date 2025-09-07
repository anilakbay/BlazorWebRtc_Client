using BlazorWebRtc_Application.Features.Commands.Upload;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        private IWebHostEnvironment _webHostEnvironment;
        public UploadController(IUploadService uploadService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _uploadService = uploadService;
        }




        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] UploadFileModel model)
        {

            if (model.File == null || model.File.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "profile-pictures");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Path.GetRandomFileName() + Path.GetExtension(model.File.FileName);

            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            var imageUrl = $"images/profile-pictures/{fileName}";

            UploadCommand command = new UploadCommand();
            command.file = imageUrl;
            command.UserId = Guid.Parse(model.UserId);

            var result = await _uploadService.UploadFile(command);
            return Ok(result);
        }

    }
}