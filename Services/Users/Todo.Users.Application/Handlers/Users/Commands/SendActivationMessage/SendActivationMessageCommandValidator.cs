using FluentValidation;

namespace Todo.Users.Application.Handlers.Users.Commands.SendActivationMessage
{
    public class SendActivationMessageCommandValidator : AbstractValidator<SendActivationMessageCommand>
    {
        public SendActivationMessageCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
