using System;
using FluentAssertions;
using webapi.Applications.AuthorOperations.Commands.UpdateAuthor;
using webapi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private UpdateAuthorCommandValidator _validator;

    public UpdateAuthorCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenAuthorIdIsInvalid_Validator_ShouldHaveError(int authorId)
    {
        // arrange
        var model = new UpdateAuthorModel
        {
            FirstName = "Furkan",
            LastName = "AKKULAK",
            BirthDate = new DateTime(2000, 07, 06)
        };
        UpdateAuthorCommand command = new(null);
        command.Model = model;
        command.AuthorId = authorId;

        // act
        var result = _validator.Validate(command);

        // assert
        result.Errors.Should().ContainSingle();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("123", "")]
    public void WhenModelIsInvalid_Validator_ShouldHaveError(string name, string surname)
    {
        // arrange
        var model = new UpdateAuthorModel
        {
            FirstName = name,
            LastName = surname,
            BirthDate = new DateTime(2000, 11, 22)
        };
        UpdateAuthorCommand updateCommand = new(null);
        updateCommand.AuthorId = 3;
        updateCommand.Model = model;

        // act
        var result = _validator.Validate(updateCommand);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenVirthDayEqualNowGiven_Validator_ShouldBeReturnError()
    {
        // arrange
        UpdateAuthorCommand command = new(null);
        command.Model = new UpdateAuthorModel
        {
            FirstName = "Burak",
            LastName = "Çetinkaya",
            BirthDate = DateTime.Now.Date
        };

        // act
        UpdateAuthorCommandValidator validator = new();
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenInputsAreValid_Validator_ShouldNotHaveError()
    {
        // arrange
        var model = new UpdateAuthorModel
        {
            FirstName = "Furkan",
            LastName = "AKKULAK",
            BirthDate = new DateTime(2000, 07, 06)
        };
        UpdateAuthorCommand updateCommand = new(null);
        updateCommand.AuthorId = 2;
        updateCommand.Model = model;

        // act
        var result = _validator.Validate(updateCommand);

        // assert
        result.Errors.Count.Should().Be(0);
    }
}
