using KPI_BACKEND.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace KPI_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentDevelopmentController : Controller
    {
        private readonly IStudentDevelopment studentDevelopmentRepository;

        //private readonly IMemoryCache _memoryCache;

        public StudentDevelopmentController(IStudentDevelopment studentDevelopmentrepository)
        {
            this.studentDevelopmentRepository = studentDevelopmentrepository;
            //this._memoryCache = memoryCache;
        }

        [HttpGet("StudentDevelopment")]
        public async Task<ActionResult> Researches()
        {
            var cacheKey = "MyKey";
            try
            {
                /*  var employeeList = await loginRepository.Getlogindetails();*/
                return Ok(await studentDevelopmentRepository.StudentDevelopment());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("SaveStudentDevelopment")]
        [RequestSizeLimit(3 * 1024 * 1024)] // 3MB max
        public async Task<IActionResult> SaveStudentDevelopment([FromForm] tbl_StudentDevelopment studentDevelopment, IFormFile file)
        {
            try
            {
                if (studentDevelopment == null || file == null || file.Length == 0)
                    return BadRequest("Invalid studentDevelopment or file data.");

                if (file.Length > 3 * 1024 * 1024)
                    return BadRequest("File size exceeds 3MB.");

                var allowedExtensions = new[] { ".pdf", ".docx", ".doc", ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(fileExtension))
                    return BadRequest("Unsupported file type.");

                // Sanitize original filename
                var originalFileName = Path.GetFileName(file.FileName);
                originalFileName = string.Concat(originalFileName.Split(Path.GetInvalidFileNameChars()));

                var baseFileName = Path.GetFileNameWithoutExtension(originalFileName);
                var teacherId = studentDevelopment.teacher_id?.ToString().Replace(" ", "_") ?? "unknown";

                // Add timestamp to prevent overwriting existing files
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                var fileName = $"{baseFileName}_{timestamp}{fileExtension}";

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", teacherId);
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                studentDevelopment.file_path = $"/Uploads/{teacherId}/{fileName}";



                await studentDevelopmentRepository.SaveStudentDevelopment(studentDevelopment);

                return Ok(studentDevelopment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data");
            }
        }

        [HttpGet("Download/{teacherid}/{filename}")]
        public IActionResult DownloadFile(string teacherid, string filename)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", teacherid, filename);

                if (System.IO.File.Exists(filePath))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(filePath);
                    var contentType = GetMimeType(filePath);
                    return File(fileBytes, contentType, filename);
                }
                else
                {
                    return NotFound("File not found.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving file.");
            }
        }

        private string GetMimeType(string filePath)
        {
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream"; // Fallback if unknown
            }
            return contentType;
        }

    }
}
