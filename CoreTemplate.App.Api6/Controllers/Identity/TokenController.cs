using CoreTemplate.App.Entites.SharedModels;
using CoreTemplate.App.External.Email;
using CoreTemplate.App.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreTemplate.App.Api.Controllers.v1.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private IMailService _mailService;

        public TokenController(IIdentityService identityService , IMailService mailService) {
            _identityService = identityService;
            _mailService = mailService;
        }
        // POST api/<TokenController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    await _mailService.SendEmailAsync(model.Email, "New login", "<h1>Hey!, new login to your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>");
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
    }
}
