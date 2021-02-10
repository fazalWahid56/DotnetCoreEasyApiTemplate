using App.Utilites.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]

    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        private readonly ILogger<ErrorsController> _logger;
        public ErrorsController(ILogger<ErrorsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into ErrorsController");
        }

        [Route("error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // Your exception
                                            // _logger.LogError(exception, exception.Message);
            _logger.LogError(exception.Message);


            var code = 500; // Internal Server Error by default
            if (exception is HttpStatusException httpStatusException)
            {
                code = (int)httpStatusException.Status;
            }

            if (exception is NotFoundException) code = 404; // Not Found
            else if (exception is MyUnauthException) code = 401; // Unauthorized
            else if (exception is CustomException) code = 400; // Bad Request

            Response.StatusCode = code; // You can use HttpStatusCode enum instead
            _logger.LogInformation("Status Code: " + code + " returned to user. ");
            if (code == 500)
            {
                return StatusCode(code, "Somthing Went Wrong. Please contact administrator.");
            }         
            return StatusCode(code, exception.Message);
        }

    }
}

