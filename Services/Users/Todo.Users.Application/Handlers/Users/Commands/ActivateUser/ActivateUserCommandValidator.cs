using FluentValidation;

namespace Todo.Users.Application.Handlers.Users.Commands.ActivateUser
{
    public class ActivateUserCommandValidator : AbstractValidator<ActivateUserCommand>
    {
        public ActivateUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
