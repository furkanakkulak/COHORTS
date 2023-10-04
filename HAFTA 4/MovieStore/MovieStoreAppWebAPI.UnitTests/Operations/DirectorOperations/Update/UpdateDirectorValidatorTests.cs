using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.DirectorOperation.Create;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Delete;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Update;

using System.Drawing;

namespace MovieStoreAppWebAPI.UnitTests.Operations.DirectorOperations.Create
{
    public class UpdateDirectorValidatorTests
    {


        [InlineData(-1, "Lorem ipsum", null)]
        [InlineData(-1, null, "Lorem ipsum")]
        [InlineData(-1, "Lorem ipsum", "Lorem ipsum")]

        [InlineData(1, null, null)]
        [InlineData(1, null, "")]
        [InlineData(1, null, "T")]
        [InlineData(1, null, "Lorem ipsum dolor sit amet, consectetuer adipiscing eli")]
        [InlineData(1, "Lorem ipsum dolor sit amet, consectetuer adipiscing eli", null)]
        [InlineData(1, "T", null)]
        [InlineData(1, "", null)]
        [InlineData(1, "", "")]
        [InlineData(1, "T", "T")]
        [InlineData(1, "Lorem ipsum dolor sit amet, consectetuer adipiscing eli", "Lorem ipsum dolor sit amet, consectetuer adipiscing eli")]

        [InlineData(0, null, null)]
        [InlineData(0, null, "")]
        [InlineData(0, null, "T")]
        [InlineData(0, null, "Lorem ipsum dolor sit amet, consectetuer adipiscing eli")]
        [InlineData(0, "Lorem ipsum dolor sit amet, consectetuer adipiscing eli", null)]
        [InlineData(0, "T", null)]
        [InlineData(0, "", null)]
        [InlineData(0, "", "")]
        [InlineData(0, "T", "T")]
        [InlineData(0, "Lorem ipsum dolor sit amet, consectetuer adipiscing eli", "Lorem ipsum dolor sit amet, consectetuer adipiscing eli")]

        [InlineData(-1, null, null)]
        [InlineData(-1, null, "")]
        [InlineData(-1, null, "T")]
        [InlineData(-1, null, "Lorem ipsum dolor sit amet, consectetuer adipiscing eli")]
        [InlineData(-1, "Lorem ipsum dolor sit amet, consectetuer adipiscing eli", null)]
        [InlineData(-1, "T", null)]
        [InlineData(-1, "", null)]
        [InlineData(-1, "", "")]
        [InlineData(-1, "T", "T")]
        [InlineData(-1, "Lorem ipsum dolor sit amet, consectetuer adipiscing eli", "Lorem ipsum dolor sit amet, consectetuer adipiscing eli")]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id,string name,string surname)
        {
            UpdateDirectorViewModelValidator validator = new UpdateDirectorViewModelValidator();

            ValidationResult result = validator.Validate(new UpdateDirectorViewModel()
            {
                Id = id,
                Name = name,
                Surname = surname
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
