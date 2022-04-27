using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Todo.Users.Api.Controllers.Base;
using Todo.Users.Api.ViewModels;
using Todo.Users.Application.Handlers.Users.Commands.CreateUser;
using Todo.Users.Application.Handlers.Users.Commands.DeleteUser;
using Todo.Users.Application.Handlers.Users.Queries.GetSuccessToken;
using Todo.Users.Application.Handlers.Users.Queries.GetUserDetails;

namespace Todo.Users.Api.Controllers
{
    public class UsersController : BaseController
    {
        [ApiVersion("1.0")]
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(GetUserDetailsVm), 200)]
        public async Task<ActionResult<GetUserDetailsVm>> Get([FromRoute] Guid userId)
        {
            GetUserDetailsQuery query = new GetUserDetailsQuery() { UserId = userId };

            return Ok(await Mediator.Send(query));
        }


        [HttpPost("login")]
        [ApiVersion("1.0")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(LoginUserVm), 200)]
        public async Task<ActionResult<LoginUserVm>> Login([FromForm] GetSuccessTokenQuery query)
        {
            string token = await Mediator.Send(query);

            return Ok(new LoginUserVm(token, UserId));
        }

        [ApiVersion("1.0")]
        [HttpPost("register")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<ActionResult<Guid>> Register([FromForm] CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpDelete]
        [ApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Delete()
        {
            DeleteUserCommand command = new DeleteUserCommand() { UserId = UserId };

            await Mediator.Send(command);

            return Ok();
        }


    }
}
