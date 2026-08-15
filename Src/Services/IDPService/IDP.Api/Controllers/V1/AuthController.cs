using Asp.Versioning;
using IDP.Application.Commands.Auth;
using IDP.Application.Queries.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class AuthController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthQuery authQuery)
        {
            var result = await _mediator.Send(authQuery);
            return Ok(result);
        }

        [HttpPost("RegisterAndSendOtp")]
        public async Task<IActionResult> RegisterAndSendOtp([FromBody] AuthCommand authCommand)
        {
            var result= await _mediator.Send(authCommand); 
            return Ok(result);
        }
    }
}
