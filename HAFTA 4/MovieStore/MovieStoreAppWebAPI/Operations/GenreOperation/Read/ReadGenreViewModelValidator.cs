using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.GenreOperation.Read
{
    public class ReadGenreViewModelValidator : AbstractValidator<ReadGenreViewModel>
    {
        public ReadGenreViewModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
