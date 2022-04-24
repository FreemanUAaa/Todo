using FluentValidation;

namespace Todo.Users.Application.Helpers.Users.Commands.ActivateUser
{
    public class ActivateUserCommandValidator : AbstractValidator<ActivateUserCommand>
    {
        public ActivateUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
