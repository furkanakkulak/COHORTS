using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.DirectorOperation.Read;

namespace MovieStoreAppWebAPI.UnitTests.Operations.DirectorOperations.Create
{
    public class ReadDirectorValidatorTests
    {

        [InlineData(0)]
        [InlineData(-1)]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id)
        {
            ReadDirectorViewModelValidator validator = new ReadDirectorViewModelValidator();

            ValidationResult result = validator.Validate(new ReadDirectorViewModel()
            {
                Id = id,
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
