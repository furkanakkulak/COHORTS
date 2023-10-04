using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.DirectorOperation.Create;

namespace MovieStoreAppWebAPI.UnitTests.Operations.DirectorOperations.Create
{
    public class CreateDirectorValidatorTests
    {

        [InlineData("","")]
        [InlineData(null,null)]
        [InlineData("", null)]
        [InlineData(null, "")]
        [InlineData("aa", "aa")]
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing eli", "Lorem ipsum dolor sit amet, consectetuer adipiscing eli")]
        [InlineData("aa", "Lorem ipsum dolor sit amet, consectetuer adipiscing eli")]
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing eli", "aa")]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name,string surname)
        {
            CreateDirectorViewModelValidator validator = new CreateDirectorViewModelValidator();

            ValidationResult result = validator.Validate(new CreateDirectorViewModel()
            {
                Name = name,
                Surname = surname
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
