using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.FilmOperation.Create;
using MovieStoreAppWebAPI.Operations.FilmOperation.Delete;

namespace MovieStoreAppWebAPI.UnitTests.Operations.FilmOperations.Create
{
    public class DeleteFilmValidatorTests
    {

        [InlineData(0)]
        [InlineData(-1)]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id)
        {
            DeleteFilmViewModelValidator validator = new DeleteFilmViewModelValidator();

            ValidationResult result = validator.Validate(new DeleteFilmViewModel()
            {
                Id = id,
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
