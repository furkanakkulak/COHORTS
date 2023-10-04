using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using webapi.Applications.BookOperations.Commands.UpdateBook;
using webapi.DBOperations;
using webapi.Entities;
using webapi.UnitTests.TestSetup;

namespace webapi.UnitTests.Applications.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)

        UpdateBookCommand command = new(context);
        command.BookId = 999;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Book not found");

        // act (Çalıştırma)

        // assert (Doğrulama)
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
    {
        // arrange
        UpdateBookCommand command = new(context);
        var book = new Book
        {
            Title = "Test Book",
            GenreId = 1,
            AuthorId = 1,
            PageCount = 100,
            PublishDate = new DateTime(2022, 1, 1)
        };

        context.Books.Add(book);
        context.SaveChanges();

        command.BookId = book.Id;
        UpdateBookModel model = new UpdateBookModel
        {
            Title = "Updated Title",
            GenreId = 2,
            AuthorId = 2
        };
        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var updatedBook = context.Books.SingleOrDefault(b => b.Id == book.Id);
        updatedBook.Should().NotBeNull();
        updatedBook.PageCount.Should().Be(book.PageCount);
        updatedBook.PublishDate.Should().Be(book.PublishDate);
        updatedBook.Title.Should().Be(model.Title);
        updatedBook.GenreId.Should().Be(model.GenreId);
        updatedBook.AuthorId.Should().Be(model.AuthorId);
    }
}
