using FluentValidation;

namespace Todo.Users.Application.Helpers.Users.Commands.SendActivationMessage
{
    public class SendActivationMessageCommandValidator : AbstractValidator<SendActivationMessageCommand>
    {
        public SendActivationMessageCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
