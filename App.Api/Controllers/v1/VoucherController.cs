using App.Entites.DTO;
using App.Services;
using App.Services.GeneralLeadger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService accService) {
            _voucherService = accService;
        }
        // GET: api/<ChartOfAccount>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _voucherService.GetAllVouchersAsync());
        }

        // GET api/<ChartOfAccount>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _voucherService.GetVoucherAsync(id));
        }

        // POST api/<ChartOfAccount>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VoucherDTO value)
        {
            if (ModelState.IsValid)
            {
                var acc = await _voucherService.CreateVoucherAsync(value);
                // Return 201
                return new ObjectResult(acc) { StatusCode = StatusCodes.Status201Created };
            }
            return BadRequest(ModelState);            
        }

        // PUT api/<ChartOfAccount>/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] VoucherDTO value)
        {
            if (ModelState.IsValid)
            {
                // Return 201
                value.VoucherId = id;
                return Ok(await _voucherService.UpdateVoucherAsync(value));
            }
            return BadRequest(ModelState);
        }

        // DELETE api/<ChartOfAccount>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _ = await _voucherService.DeleteVoucherAsync(id);
            return NoContent();
        }
    }
}
