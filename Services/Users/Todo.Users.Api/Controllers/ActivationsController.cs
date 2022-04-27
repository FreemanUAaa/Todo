using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Users.Api.Controllers.Base;
using Todo.Users.Application.Handlers.Users.Commands.ActivateUser;
using Todo.Users.Application.Handlers.Users.Commands.SendActivationMessage;

namespace Todo.Users.Api.Controllers
{
    public class ActivationsController : BaseController
    {
        [ApiVersion("1.0")]
        [HttpGet("/activate/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Activate([FromForm] ActivateUserCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [ApiVersion("1.0")]
        [HttpPost("/send/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Send([FromForm] SendActivationMessageCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }
    }
}
