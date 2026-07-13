using IDP.Api.Controllers.BaseController;
using IDP.Application.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
     public class UserController(IMediator _mediator) : IBaseController
    {
        /// <summary>
        /// enter user info
        /// </summary>
        /// <returns></returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] UserCommand userCommand)
        {
            var result = await _mediator.Send(userCommand);
            return Ok(result);
        }


    }
}
