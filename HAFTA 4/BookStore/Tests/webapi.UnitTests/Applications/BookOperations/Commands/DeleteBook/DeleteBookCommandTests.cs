using System;
using System.Linq;
using FluentAssertions;
using webapi.Applications.BookOperations.Commands.DeleteBook;
using webapi.DBOperations;
using webapi.UnitTests.TestSetup;

namespace webapi.UnitTests.Applications.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)

        DeleteBookCommand command = new(context);
        command.BookId = 9999999;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Book not found");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
    {
        // arrange
        DeleteBookCommand command = new(context);
        command.BookId = 2;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var book = context.Books.SingleOrDefault(b => b.Id == command.BookId);
        book.Should().BeNull();
    }
}
