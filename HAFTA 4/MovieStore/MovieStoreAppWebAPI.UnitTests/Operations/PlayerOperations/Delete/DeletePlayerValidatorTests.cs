using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.PlayerOperation.Create;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Delete;

namespace MovieStoreAppWebAPI.UnitTests.Operations.PlayerOperations.Create
{
    public class DeletePlayerValidatorTests
    {

        [InlineData(0)]
        [InlineData(-1)]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id)
        {
            DeletePlayerViewModelValidator validator = new DeletePlayerViewModelValidator();

            ValidationResult result = validator.Validate(new DeletePlayerViewModel()
            {
                Id = id,
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
