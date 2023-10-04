using AutoMapper;
using FluentAssertions;
using webapi.Applications.AuthorOperations.Queries.GetAuthor;
using webapi.DBOperations;
using webapi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthors;

public class GetAuthorsQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetAuthorsQueryTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGetAuthorsQueryIsHandled_AuthorListShouldBeReturned()
    {
        // Arrange
        var query = new GetAuthorsQuery(context, mapper);

        // Act
        var result = query.Handle();

        // Assert
        result.Should().NotBeNull();
    }
}
