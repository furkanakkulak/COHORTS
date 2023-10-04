using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.GenreOperation.Create;
using MovieStoreAppWebAPI.Operations.GenreOperation.Delete;
using MovieStoreAppWebAPI.Operations.GenreOperation.Update;

namespace MovieStoreAppWebAPI.UnitTests.Operations.GenreOperations.Create
{
    public class UpdateGenreValidatorTests
    {

        [InlineData(1, null)]
        [InlineData(1, "")]
        [InlineData(1, "T")]
        [InlineData(1,"Lorem ipsum dolor sit amet, consectetuer adipiscing eli")]
        [InlineData(-1, "Lorem ipsum")]
        [InlineData(0, "Lorem ipsum")]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id, string name)
        {
            UpdateGenreViewModelValidator validator = new UpdateGenreViewModelValidator();

            ValidationResult result = validator.Validate(new UpdateGenreViewModel()
            {
                Name = name,
                Id = id
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
