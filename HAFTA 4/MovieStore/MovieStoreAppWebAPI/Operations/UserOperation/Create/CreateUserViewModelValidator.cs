using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.UserOperation.Create
{
    public class CreateUserViewModelValidator : AbstractValidator<CreateUserViewModel>
    {
        public CreateUserViewModelValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(x => x.Username)
                .MinimumLength(2)
                .WithMessage("Minimum length of username must be 2");
            RuleFor(x => x.Username)
                .MaximumLength(30)
                .WithMessage("Maximum length of username must be 30");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Minimum length of name must be 2");
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Maximum length of name must be 100");

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname cannot be empty");
            RuleFor(x => x.Surname)
                .MinimumLength(2)
                .WithMessage("Minimum length of surname must be 2");
            RuleFor(x => x.Surname)
                .MaximumLength(100)
                .WithMessage("Maximum length of surname must be 100");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email must be valid");
            RuleFor(x => x.Email)
                .MinimumLength(10)
                .WithMessage("Minimum length of email must be 100");
            RuleFor(x => x.Email)
                .MaximumLength(100)
                .WithMessage("Maximum length of email must be 100");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(x => x.Password);

            RuleForEach(x => x.FavouriteGenreIds)
                .NotEqual(0)
                .WithMessage("Favourite genre ID cannot be empty");
            RuleForEach(x => x.FavouriteGenreIds)
                .GreaterThan(0)
                .WithMessage("Favourite genre ID must be greater than 0");
        }
    }
}
