using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.PlayerOperation.Read;

namespace MovieStoreAppWebAPI.UnitTests.Operations.PlayerOperations.Create
{
    public class ReadPlayerValidatorTests
    {

        [InlineData(0)]
        [InlineData(-1)]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id)
        {
            ReadPlayerViewModelValidator validator = new ReadPlayerViewModelValidator();

            ValidationResult result = validator.Validate(new ReadPlayerViewModel()
            {
                Id = id,
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
