using Microsoft.AspNetCore.Mvc;
using Taxes.Services;

namespace Taxes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportController : Controller
    {
        private readonly IImportService importService;

        public ImportController(IImportService importService)
        {
            this.importService = importService;
        }

        [HttpPost("ImportMunicipalities")]
        public async Task<ActionResult> ImportMunicipalities(IFormFile formFile)
        {
            if (formFile == null)
            {
                return BadRequest();
            }

            try
            {
                if (!await importService.LoadFile(formFile))
                    return BadRequest();

                if (!await importService.ImportData())
                    return BadRequest();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResult(ex));
            }
        }
    }
}
