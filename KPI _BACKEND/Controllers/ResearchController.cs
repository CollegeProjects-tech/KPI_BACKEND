using KPI_BACKEND.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;


namespace KPI_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResearchController : Controller
    {
        private readonly IResearch researchRepository;

        //private readonly IMemoryCache _memoryCache;

        public ResearchController(IResearch researchrepository)
        {
            this.researchRepository = researchrepository;
            //this._memoryCache = memoryCache;
        }

        [HttpGet("Researches")]
        public async Task<ActionResult> Researches()
        {
            var cacheKey = "MyKey";
            try
            {
                /*  var employeeList = await loginRepository.Getlogindetails();*/
                return Ok(await researchRepository.Researches());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("SaveResearch")]
        [RequestSizeLimit(3 * 1024 * 1024)] // 3MB max
        public async Task<IActionResult> SaveResearch([FromForm] tbl_Research research, IFormFile file)
        {
            try
            {
                if (research == null || file == null || file.Length == 0)
                    return BadRequest("Invalid research or file data.");

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
                var teacherId = research.teacher_id?.ToString().Replace(" ", "_") ?? "unknown";

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

                research.file_path = $"/Uploads/{teacherId}/{fileName}";



                await researchRepository.SaveResearch(research);

                return Ok(research);
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
