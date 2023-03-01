using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Taxes.Entities;
using Taxes.Services;

namespace Taxes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService taxService;
        private DateTime datetax;

        public TaxController(ITaxService taxService)
        {
            this.taxService = taxService;
        }


        [HttpGet("FindTax")]
        public async Task<IActionResult> FindTaxAsync(string Municipality, string DateTax)
        {
            try
            {
                if (Municipality.IsNullOrEmpty() || DateTax.IsNullOrEmpty() || !DateTime.TryParse(DateTax, out datetax))
                    return BadRequest();

                return Ok(new JsonResult(await taxService.GetTax(Municipality, datetax)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResult(ex));
            }
        }


        [HttpGet("GetTaxTypes")]
        public async Task<IActionResult> GetTaxListAsync()
        {
            try
            {
                return Ok(new JsonResult(await taxService.GetTaxType()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResult(ex));
            }
        }

        [HttpPost("AddTax")]
        public async Task<IActionResult> AddTaxListAsync(Tax tax)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(new JsonResult(await taxService.AddTax(tax)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResult(ex));
            }
        }

    }
}
