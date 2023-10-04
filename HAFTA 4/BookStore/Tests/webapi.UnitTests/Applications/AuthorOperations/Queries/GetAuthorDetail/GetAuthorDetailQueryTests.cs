using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using webapi.Applications.AuthorOperations.Queries.GetAuthorDetail;
using webapi.DBOperations;
using webapi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeReturned()
    {
        // arrange
        GetAuthorDetailQuery query = new(context, mapper);
        var AuthorId = query.AuthorId = 1;

        var author = context.Authors.Where(a => a.Id == AuthorId).SingleOrDefault();

        // act
        AuthorDetailViewModel vm = query.Handle();

        // assert
        vm.Should().NotBeNull();
        vm.FirstName.Should().Be(author.FirstName);
        vm.LastName.Should().Be(author.LastName);
        vm.BirthDate.Should().Be(author.BirthDate);
    }

    [Fact]
    public void WhenNonExistingAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        int authorId = 9999;

        GetAuthorDetailQuery query = new(context, mapper);
        query.AuthorId = authorId;

        // assert
        query
            .Invoking(x => x.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Author not found!");
    }
}
