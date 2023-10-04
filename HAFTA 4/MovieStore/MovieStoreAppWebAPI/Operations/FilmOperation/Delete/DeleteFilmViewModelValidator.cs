using FluentValidation;

using MovieStoreAppWebAPI.Operations.FilmOperation.Delete;

namespace MovieStoreAppWebAPI.Operations.FilmOperation.Create
{
    public class DeleteFilmViewModelValidator : AbstractValidator<DeleteFilmViewModel>
    {
        public DeleteFilmViewModelValidator()
        {
            RuleFor(x => x.Id).NotEqual(0).WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
