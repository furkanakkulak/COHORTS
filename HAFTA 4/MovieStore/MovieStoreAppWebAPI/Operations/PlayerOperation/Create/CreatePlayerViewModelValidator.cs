using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.PlayerOperation.Create
{
    public class CreatePlayerViewModelValidator : AbstractValidator<CreatePlayerViewModel>
    {
        public CreatePlayerViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Minumum length of name must be 3");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Maximum length of name must be 30");

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname cannot be empty");
            RuleFor(x => x.Surname).MinimumLength(3).WithMessage("Minumum length of surname must be 3");
            RuleFor(x => x.Surname).MaximumLength(30).WithMessage("Maximum length of surname must be 30");
        }
    }
}
