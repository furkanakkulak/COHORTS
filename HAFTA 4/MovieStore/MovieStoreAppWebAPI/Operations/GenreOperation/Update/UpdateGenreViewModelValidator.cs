using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.GenreOperation.Update
{
    public class UpdateGenreViewModelValidator : AbstractValidator<UpdateGenreViewModel>
    {
        public UpdateGenreViewModelValidator()
        {
            RuleFor(x => x.Id).NotEqual(0).WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Minimum length of name must be 3");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Maximum length of name must be 50");
        }
    }
}
