using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.FilmOperation.Create
{
    public class CreateFilmViewModelValidator : AbstractValidator<CreateFilmViewModel>
    {
        public CreateFilmViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Minumum length of name must be 2");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Maximum length of Name must be 1000");

            RuleFor(x => x.About).NotEmpty().WithMessage("About cannot be empty");
            RuleFor(x => x.About).MinimumLength(10).WithMessage("Minumum length of about must be 10");
            RuleFor(x => x.About).MaximumLength(1000).WithMessage("Maximum length of about must be 1000");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Price cannot be empty");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.DirectorId).NotEmpty().WithMessage("Director ID cannot be empty");
            RuleFor(x => x.DirectorId).GreaterThan(0).WithMessage("Director ID must be greater than 0");

            RuleFor(x => x.GenreId).NotEmpty().WithMessage("Genre ID cannot be empty");
            RuleFor(x => x.GenreId).GreaterThan(0).WithMessage("Genre ID must be greater than 0");

            RuleForEach(x => x.PlayerIds).NotEmpty().WithMessage("Player ID cannot be empty");
            RuleForEach(x => x.PlayerIds).GreaterThan(0).WithMessage("Player ID must be greater than 0");

            RuleFor(x => x.PublishedDate).NotEqual(new DateTime(0001,01,01)).WithMessage("Published date cannot be empty");
            RuleFor(x => x.PublishedDate).LessThan(DateTime.Now).WithMessage("Published date must be valid");
        }
    }
}
