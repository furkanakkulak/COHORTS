using FluentValidation;

using MovieStoreAppWebAPI.Operations.PlayerOperation.Update;

namespace MovieStoreAppWebAPI.Operations.PlayerOperation.Create
{
    public class UpdatePlayerViewModelValidator : AbstractValidator<UpdatePlayerViewModel>
    {
        public UpdatePlayerViewModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Minumum length of name must be 3");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Maximum length of name must be 30");

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname cannot be empty");
            RuleFor(x => x.Surname).MinimumLength(3).WithMessage("Minumum length of surname must be 3");
            RuleFor(x => x.Surname).MaximumLength(30).WithMessage("Maximum length of surname must be 30");
        }
    }
}
