using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.PlayerOperation.Create;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Delete;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Update;

using System.Drawing;

namespace MovieStoreAppWebAPI.UnitTests.Operations.PlayerOperations.Create
{
    public class UpdatePlayerValidatorTests
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
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id, string name,string surname)
        {
            UpdatePlayerViewModelValidator validator = new UpdatePlayerViewModelValidator();

            ValidationResult result = validator.Validate(new UpdatePlayerViewModel()
            {
                Name = name,
                Surname = surname,
                Id = id
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
