using KPI_BACKEND.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace KPI_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdmin adminRepository;

        //private readonly IMemoryCache _memoryCache;

        public AdminController(IAdmin adminrepository)
        {
            this.adminRepository = adminrepository;
            //this._memoryCache = memoryCache;
        }

        [HttpGet("Users")]
        public async Task<ActionResult> Users()
        {
            var cacheKey = "MyKey";
            try
            {
                /*  var employeeList = await loginRepository.Getlogindetails();*/
                return Ok(await adminRepository.Users());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Route("SaveUser")]
        public async Task<ActionResult> SaveUser(tbl_Users users)
        {
            try
            {
                if (users == null)
                {
                    return BadRequest("User data is null.");
                }

                // Await the save operation and get the result code (int)
                int result = await adminRepository.SaveUsers(users);

                if (result == 1)
                    return Ok("User saved successfully.");
                else if (result == 2)
                    return Conflict("User Id already exists.");
                else
                    return BadRequest("Failed to save user.");

            }
            catch (Exception ex)
            {
                // Optionally log ex.Message here
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data");
            }
        }

    }
}
