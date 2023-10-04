using FluentAssertions;
using webapi.Applications.GenreOperations.Commands.CreateGenre;
using webapi.UnitTests.TestSetup;
using Xunit;

namespace webapi.UnitTests.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("a b")]
        [InlineData("a")]
        [InlineData("ab")]
        public void WhenInvalidInputsGiven_Validator_ShouldBeReturnErrors(String name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel() { Name = name };

            CreateGenreCommandValidator validations = new CreateGenreCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        public void WhenInvalidInputsGiven_Validator_ShouldBeReturn(String name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel() { Name = name };

            CreateGenreCommandValidator validations = new CreateGenreCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
