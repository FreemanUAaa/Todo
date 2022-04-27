using System;

namespace Todo.Users.Api.ViewModels
{
    public class LoginUserVm
    {
        public string SuccessToken { get; set; }

        public Guid UserId { get; set; }

        public LoginUserVm(string successToken, Guid userId) =>
            (SuccessToken, UserId) = (successToken, userId);
    }
}
