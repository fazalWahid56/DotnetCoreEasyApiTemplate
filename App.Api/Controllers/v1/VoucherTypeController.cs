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
    public class VoucherTypeController : ControllerBase
    {
        private readonly IVoucherService _vouchTypeService;
        public VoucherTypeController(IVoucherService accService) {
            _vouchTypeService = accService;
        }
        // GET: api/<ChartOfAccount>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _vouchTypeService.GetAllVoucherTypesAsync());
        }

        // GET api/<ChartOfAccount>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _vouchTypeService.GetVoucherTypeAsync(id));
        }

        // POST api/<ChartOfAccount>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VoucherTypeDTO value)
        {
            if (ModelState.IsValid)
            {
                var acc = await _vouchTypeService.CreateVoucherTypeAsync(value);
                // Return 201
                return new ObjectResult(acc) { StatusCode = StatusCodes.Status201Created };
            }
            return BadRequest(ModelState);            
        }

        // PUT api/<ChartOfAccount>/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] VoucherTypeDTO value)
        {
            if (ModelState.IsValid)
            {
                // Return 201
                value.VoucherTypeId = id;
                return Ok(await _vouchTypeService.UpdateVoucherTypeAsync(value));
            }
            return BadRequest(ModelState);
        }

        // DELETE api/<ChartOfAccount>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _ = await _vouchTypeService.DeleteVoucherTypeAsync(id);
            return NoContent();
        }
    }
}
