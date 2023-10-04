using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.FilmOperation.Read;

namespace MovieStoreAppWebAPI.UnitTests.Operations.FilmOperations.Create
{
    public class ReadFilmValidatorTests
    {

        [InlineData(0)]
        [InlineData(-1)]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id)
        {
            ReadFilmViewModelValidator validator = new ReadFilmViewModelValidator();

            ValidationResult result = validator.Validate(new ReadFilmViewModel()
            {
                Id = id,
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
