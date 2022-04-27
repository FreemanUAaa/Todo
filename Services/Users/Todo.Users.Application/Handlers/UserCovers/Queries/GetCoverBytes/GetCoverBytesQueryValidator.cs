using FluentValidation;

namespace Todo.Users.Application.Handlers.UserCovers.Queries.GetCoverBytes
{
    public class GetCoverBytesQueryValidator : AbstractValidator<GetCoverBytesQuery>
    {
        public GetCoverBytesQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
