using KPI_BACKEND.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace KPI_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogin loginRepository;

        public LoginController(ILogin loginrepository)
        {
            this.loginRepository = loginrepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Login/{userid}/{password}")]
        public async Task<ActionResult> Login(int userid , string password)
        {
            try
            {
                //ExportQRs objectExport = new ExportQRs();
                //objectExport.generatePdf();


                var result = await loginRepository.Login(userid, password);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                return Ok(result);
                //return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
