using FluentValidation;

using MovieStoreAppWebAPI.Operations.GenreOperation.Delete;

namespace MovieStoreAppWebAPI.Operations.GenreOperation.Create
{
    public class DeleteGenreViewModelValidator : AbstractValidator<DeleteGenreViewModel>
    {
        public DeleteGenreViewModelValidator()
        {
            RuleFor(x => x.Id).NotEqual(0).WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
