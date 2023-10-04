using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.DirectorOperation.Create;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Delete;

namespace MovieStoreAppWebAPI.UnitTests.Operations.DirectorOperations.Create
{
    public class DeleteDirectorValidatorTests
    {

        [InlineData(0)]
        [InlineData(-1)]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id)
        {
            DeleteDirectorViewModelValidator validator = new DeleteDirectorViewModelValidator();

            ValidationResult result = validator.Validate(new DeleteDirectorViewModel()
            {
                Id = id,
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
