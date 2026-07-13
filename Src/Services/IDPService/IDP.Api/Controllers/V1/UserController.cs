using Asp.Versioning;
using IDP.Api.Controllers.BaseController;
using IDP.Application.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]

    public class UserController(IMediator _mediator) : IBaseController
    {
        /// <summary>
        /// enter user info
        /// </summary>
        /// <returns></returns>
        [MapToApiVersion(1)]
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] UserCommand userCommand)
        {
            var result = await _mediator.Send(userCommand);
            return Ok(result);
        }


    }
}
