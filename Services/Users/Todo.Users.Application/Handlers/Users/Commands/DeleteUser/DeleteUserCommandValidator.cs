﻿using FluentValidation;

namespace Todo.Users.Application.Handlers.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
