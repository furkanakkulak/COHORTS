using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.PlayerOperation.Create;

namespace MovieStoreAppWebAPI.UnitTests.Operations.PlayerOperations.Create
{
    public class CreatePlayerValidatorTests
    {

        [InlineData("","")]
        [InlineData(null,null)]
        [InlineData("", null)]
        [InlineData(null, "")]
        [InlineData("aa", "aa")]
        [InlineData("Lorem ipsum dolor sit amet, consect", "Lorem ipsum dolor sit amet, consect")]
        [InlineData("aa", "Lorem ipsum dolor sit amet")]
        [InlineData("Lorem ipsum dolor sit amet", "aa")]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name,string surname)
        {
            CreatePlayerViewModelValidator validator = new CreatePlayerViewModelValidator();

            ValidationResult result = validator.Validate(new CreatePlayerViewModel()
            {
                Name = name,
                Surname = surname
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
