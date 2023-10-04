using System;
using FluentAssertions;
using webapi.Applications.AuthorOperations.Commands.CreateAuthor;
using webapi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("")]
    [InlineData("0")]
    [InlineData("a")]
    public void WhenNameIsInvalid_Validator_ShouldReturnError(string name)
    {
        // arrange
        var command = new CreateAuthorCommand(null, null);
        var Model = new CreateAuthorModel
        {
            FirstName = name,
            LastName = "Test",
            BirthDate = new DateTime(1990, 1, 1)
        };

        command.Model = Model;

        var validator = new CreateAuthorCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData("")]
    [InlineData("0")]
    [InlineData("a")]
    public void WhenSurnameHasLessThan4Characters_Validator_ShouldReturnError(string surname)
    {
        // arrange
        var command = new CreateAuthorCommand(null, null);
        var Model = new CreateAuthorModel
        {
            FirstName = "Deneme",
            LastName = surname,
            BirthDate = new DateTime(1990, 1, 1)
        };

        command.Model = Model;

        var validator = new CreateAuthorCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenBirthdayIsAfterToday_Validator_ShouldReturnError()
    {
        // arrange
        var command = new CreateAuthorCommand(null, null);
        var Model = new CreateAuthorModel
        {
            FirstName = "Deneme",
            LastName = "Test",
            BirthDate = DateTime.Now.AddDays(1)
        };

        command.Model = Model;

        var validator = new CreateAuthorCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.Errors.Should().ContainSingle();
    }

    [Fact]
    public void WhenModelIsValid_Validator_ShouldNotReturnError()
    {
        // arrange
        var command = new CreateAuthorCommand(null, null);
        var Model = new CreateAuthorModel
        {
            FirstName = "Deneme",
            LastName = "Test",
            BirthDate = new DateTime(1990, 1, 1)
        };

        command.Model = Model;

        var validator = new CreateAuthorCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.Errors.Should().BeEmpty();
    }
}
