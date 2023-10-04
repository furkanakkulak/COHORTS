using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.FilmOperation.Read
{
    public class ReadFilmViewModelValidator : AbstractValidator<ReadFilmViewModel>
    {
        public ReadFilmViewModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
