using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.AuthOperation.Login
{
    public class UserLoginViewModelValidator : AbstractValidator<UserLoginViewModel>
    {
        public UserLoginViewModelValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}
