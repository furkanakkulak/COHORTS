using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using webapi.Applications.BookOperations.Queries.GetBookDetail;
using webapi.DBOperations;
using webapi.UnitTests.TestSetup;

namespace webapi.UnitTests.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeReturned()
    {
        // arrange
        GetBookDetailQuery query = new(context, mapper);
        var BookId = query.BookId = 1;

        var book = context.Books
            .Include(x => x.Genre)
            .Include(x => x.Author)
            .Where(b => b.Id == BookId)
            .SingleOrDefault();

        // act
        BookDetailViewModel vm = query.Handle();

        // assert
        vm.Should().NotBeNull();
        vm.Title.Should().Be(book.Title);
        vm.PageCount.Should().Be(book.PageCount);
        vm.Genre.Should().Be(book.Genre.Name);
        vm.Author.Should().Be(book.Author.FirstName + " " + book.Author.LastName);
        vm.PublishDate.Should().Be(book.PublishDate.ToString("dd/MM/yyyy"));
    }

    [Fact]
    public void WhenNonExistingBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        int bookId = 99999;

        GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
        query.BookId = bookId;

        // assert
        query
            .Invoking(x => x.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Book not found");
    }
}
