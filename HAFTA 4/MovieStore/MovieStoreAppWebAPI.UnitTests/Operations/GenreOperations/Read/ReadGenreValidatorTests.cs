using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.GenreOperation.Create;
using MovieStoreAppWebAPI.Operations.GenreOperation.Delete;
using MovieStoreAppWebAPI.Operations.GenreOperation.Read;

namespace MovieStoreAppWebAPI.UnitTests.Operations.GenreOperations.Create
{
    public class ReadGenreValidatorTests
    {

        [InlineData(0)]
        [InlineData(-1)]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id)
        {
            ReadGenreViewModelValidator validator = new ReadGenreViewModelValidator();

            ValidationResult result = validator.Validate(new ReadGenreViewModel()
            {
                Id = id,
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
