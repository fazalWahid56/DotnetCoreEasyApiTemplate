namespace CoreTemplate.App.Api.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AccountNatureController : ControllerBase
{
private readonly IAccountService _accNatureService;
public AccountNatureController(IAccountService accService) {
    _accNatureService = accService;
}
// GET: api/<ChartOfAccount>
[HttpGet]
public async Task<IActionResult> Get()
{
    return Ok(await _accNatureService.GetAllAccountNaturesAsync());
}

// GET api/<ChartOfAccount>/5
[HttpGet("{id}")]
public async Task<IActionResult> Get(int id)
{
    return Ok(await _accNatureService.GetAccountNatureAsync(id));
}

// POST api/<ChartOfAccount>
[HttpPost]
public async Task<IActionResult> Post([FromBody] AccountNatureDTO value)
{
    if (ModelState.IsValid)
    {
        var acc = await _accNatureService.CreateAccountNatureAsync(value);
        // Return 201
        return new ObjectResult(acc) { StatusCode = StatusCodes.Status201Created };
    }
    return BadRequest(ModelState);            
}

// PUT api/<ChartOfAccount>/5
[HttpPatch("{id}")]
public async Task<IActionResult> Patch(int id, [FromBody] AccountNatureDTO value)
{
    if (ModelState.IsValid)
    {
        // Return 201
        value.AccountNatureId = id;
        return Ok(await _accNatureService.UpdateAccountNatureAsync(value));
    }
    return BadRequest(ModelState);
}

// DELETE api/<ChartOfAccount>/5
[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    _ = await _accNatureService.DeleteAccountNatureAsync(id);
    return NoContent();
}
}

