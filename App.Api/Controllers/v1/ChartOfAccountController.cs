using App.Entites.DTO;
using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartOfAccountController : ControllerBase
    {
        private readonly IAccountService _accService;
        public ChartOfAccountController(IAccountService accService) {
            _accService = accService;
        }
        // GET: api/<ChartOfAccount>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _accService.GetAllAccountsAsync());
        }

        // GET api/<ChartOfAccount>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _accService.GetAccountAsync(id));
        }

        // POST api/<ChartOfAccount>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountDTO value)
        {
            if (ModelState.IsValid)
            {
                var acc = await _accService.CreateAccountAsync(value);
                // Return 201
                return new ObjectResult(acc) { StatusCode = StatusCodes.Status201Created };
            }
            return BadRequest(ModelState);            
        }

        // PUT api/<ChartOfAccount>/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] AccountDTO value)
        {
            if (ModelState.IsValid)
            {
                value.AccountId = id;
                // Return 201
                return Ok(await _accService.UpdateAccountAsync(value));
            }
            return BadRequest(ModelState);
        }

        // DELETE api/<ChartOfAccount>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _ = await _accService.DeleteAccountAsync(id);
            return NoContent();
        }
    }
}
