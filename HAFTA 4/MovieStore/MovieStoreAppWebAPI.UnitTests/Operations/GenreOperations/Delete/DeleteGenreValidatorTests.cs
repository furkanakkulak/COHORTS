using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.GenreOperation.Create;
using MovieStoreAppWebAPI.Operations.GenreOperation.Delete;

namespace MovieStoreAppWebAPI.UnitTests.Operations.GenreOperations.Create
{
    public class DeleteGenreValidatorTests
    {

        [InlineData(0)]
        [InlineData(-1)]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id)
        {
            DeleteGenreViewModelValidator validator = new DeleteGenreViewModelValidator();

            ValidationResult result = validator.Validate(new DeleteGenreViewModel()
            {
                Id = id,
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
