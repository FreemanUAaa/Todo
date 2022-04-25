using FluentValidation;

namespace Todo.Users.Application.Handlers.Users.Queries.GetSuccessToken
{
    public class GetSuccessTokenQueryValidator : AbstractValidator<GetSuccessTokenQuery>
    {
        public GetSuccessTokenQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            
            RuleFor(x => x.Password).NotEmpty().MinimumLength(4);
        }
    }
}
