using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.GenreOperation.Create
{
    public class CreateGenreViewModelValidator : AbstractValidator<CreateGenreViewModel>
    {
        public CreateGenreViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Minimum length of name must be 3");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Maximum length of name must be 50");
        }
    }
}
