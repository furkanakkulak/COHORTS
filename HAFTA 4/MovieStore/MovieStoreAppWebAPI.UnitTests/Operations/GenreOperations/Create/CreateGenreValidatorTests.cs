using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.GenreOperation.Create;

namespace MovieStoreAppWebAPI.UnitTests.Operations.GenreOperations.Create
{
    public class CreateGenreValidatorTests
    {

        [InlineData("")]
        [InlineData(null)]
        [InlineData("a")]
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing eli")]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
        {
            CreateGenreViewModelValidator validator = new CreateGenreViewModelValidator();

            ValidationResult result = validator.Validate(new CreateGenreViewModel()
            {
                Name = name,
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
