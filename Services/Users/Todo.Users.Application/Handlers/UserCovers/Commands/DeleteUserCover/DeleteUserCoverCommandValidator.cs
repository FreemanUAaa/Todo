using FluentValidation;

namespace Todo.Users.Application.Handlers.UserCovers.Commands.DeleteUserCover
{
    public class DeleteUserCoverCommandValidator : AbstractValidator<DeleteUserCoverCommand>
    {
        public DeleteUserCoverCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
