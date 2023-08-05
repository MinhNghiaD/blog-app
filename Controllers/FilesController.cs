using blog_app.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace blog_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        public FilesController(IFileRepository fileRepository) 
        {
            _fileRepository = fileRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file) {
            if (file == null || file.Length == 0)
            {
                // Handle the case when no file is selected for upload
                return ValidationProblem(
                    new ValidationProblemDetails
                    {
                        Status = 400, // Bad Request status code
                        Title = "Bad input.",
                        Detail = "Please correct the specified validation errors and try again.",
                    }
                );
            }

            if (file.Length > 10485760) // 10 MB (1024 * 1024 * 10)
            {
                return ValidationProblem(
                    new ValidationProblemDetails
                    {
                        Status = 400, // Bad Request status code
                        Title = "File size exceed.",
                        Detail = "Please correct the specified validation errors and try again.",
                    }
                );            
            }
            string uri =  await _fileRepository.UploadAsync(file);
            return new JsonResult(new {link = "http://localhost:8080/api/files/" + uri});
        }

        [HttpGet("{uri}")]
        public async Task<IActionResult> Get(string uri) 
        {
            byte[] imageData = await _fileRepository.GetAsync(uri);
            if (imageData == null || imageData.Length == 0) 
            {
                return NotFound();
            }

            // Create an IFormFile instance
            return File(imageData, "application/octet-stream", uri);
        }
    }
}