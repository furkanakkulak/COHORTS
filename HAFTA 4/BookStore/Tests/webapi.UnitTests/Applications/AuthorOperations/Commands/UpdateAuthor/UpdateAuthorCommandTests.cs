using System;
using System.Linq;
using FluentAssertions;
using webapi.Applications.AuthorOperations.Commands.UpdateAuthor;
using webapi.DBOperations;
using webapi.Entities;
using webapi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)

        UpdateAuthorCommand command = new(context);
        command.AuthorId = 999;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Author not found!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
    {
        // arrange
        UpdateAuthorCommand command = new(context);
        var author = new Author
        {
            FirstName = "Burak",
            LastName = "Çetinkaya",
            BirthDate = new DateTime(1999, 08, 25)
        };

        context.Authors.Add(author);
        context.SaveChanges();

        command.AuthorId = author.Id;
        UpdateAuthorModel model = new UpdateAuthorModel
        {
            FirstName = "Furkan",
            LastName = "AKKULAK",
            BirthDate = new DateTime(2000, 07, 06)
        };
        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var updatedAuthor = context.Authors.SingleOrDefault(a => a.Id == author.Id);
        updatedAuthor.Should().NotBeNull();
        updatedAuthor.FirstName.Should().Be(model.FirstName);
        updatedAuthor.LastName.Should().Be(model.LastName);
        updatedAuthor.BirthDate.Should().Be(model.BirthDate);
    }
}
