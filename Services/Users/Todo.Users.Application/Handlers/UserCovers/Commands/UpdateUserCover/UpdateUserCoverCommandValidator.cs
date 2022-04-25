using FluentValidation;

namespace Todo.Users.Application.Handlers.UserCovers.Commands.UpdateUserCover
{
    public class UpdateUserCoverCommandValidator : AbstractValidator<UpdateUserCoverCommand>
    {
        public UpdateUserCoverCommandValidator()
        {
            RuleFor(x => x.File).NotEmpty();
            
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
