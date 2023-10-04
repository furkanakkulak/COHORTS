using System;
using System.Linq;
using FluentAssertions;
using webapi.Applications.AuthorOperations.Commands.DeleteAuthor;
using webapi.DBOperations;
using webapi.Entities;
using webapi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)

        DeleteAuthorCommand command = new(context);
        command.AuthorId = 120;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Author not found!");
    }

    [Fact]
    public void WhenGivenAuthorHaveBook_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)

        DeleteAuthorCommand command = new(context);
        command.AuthorId = 1;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Author has books, cannot be deleted!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
    {
        // arrange
        var newAuthor = new Author()
        {
            FirstName = "Furkan",
            LastName = "AKKULAK",
            BirthDate = new DateTime(2000, 07, 06)
        };
        context.Authors.Add(newAuthor);
        context.SaveChanges();

        DeleteAuthorCommand command = new(context);
        command.AuthorId = newAuthor.Id;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var author = context.Authors.SingleOrDefault(a => a.Id == command.AuthorId);
        author.Should().BeNull();
    }
}
