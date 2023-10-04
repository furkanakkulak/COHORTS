using AutoMapper;
using FluentAssertions;
using webapi.Applications.BookOperations.Queries.GetBooks;
using webapi.DBOperations;
using webapi.UnitTests.TestSetup;

namespace webapi.UnitTests.Application.BookOperations.Queries.GetBooks;

public class GetBooksQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetBooksQueryTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGetBooksQueryIsHandled_BookListShouldBeReturned()
    {
        // Arrange
        var query = new GetBooksQuery(context, mapper);

        // Act
        var result = query.Handle();

        // Assert
        result.Should().NotBeNull();
    }
}
