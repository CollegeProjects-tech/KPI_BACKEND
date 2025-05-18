using KPI_BACKEND.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPI_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrincipleController : Controller
    {
        private readonly IPrinciple principleRepository;

        public PrincipleController(IPrinciple principlerepository)
        {
            this.principleRepository = principlerepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ViewDetails/{teacher_id}")]
        public async Task<ActionResult> ViewDetails(int teacher_id)
        {
            try
            {
                //ExportQRs objectExport = new ExportQRs();
                //objectExport.generatePdf();


                var result = await principleRepository.ViewDetails(teacher_id);
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

        [AllowAnonymous]
        [HttpGet]
        [Route("DipartmentsWiseTeachers/{department}")]
        public async Task<ActionResult> DipartmentsWiseTeachers(string department)
        {
            try
            {
                //ExportQRs objectExport = new ExportQRs();
                //objectExport.generatePdf();


                var result = await principleRepository.DipartmentsWiseTeachers(department);
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
