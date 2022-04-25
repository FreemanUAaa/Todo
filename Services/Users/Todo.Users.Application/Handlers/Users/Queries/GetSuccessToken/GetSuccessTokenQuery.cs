using MediatR;

namespace Todo.Users.Application.Handlers.Users.Queries.GetSuccessToken
{
    public class GetSuccessTokenQuery : IRequest<string>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
