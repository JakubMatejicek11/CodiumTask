using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared.DTOs;

namespace CodiumTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        [HttpPost("logexception")]
        public IActionResult LogClientException([FromBody] ClientErrorDTO error)
        {
            Log.Error("[Client] Client exception occurred: {Message}, StackTrace: {StackTrace}", error.Message, error.StackTrace);
            return Ok();
        }
    }
}
