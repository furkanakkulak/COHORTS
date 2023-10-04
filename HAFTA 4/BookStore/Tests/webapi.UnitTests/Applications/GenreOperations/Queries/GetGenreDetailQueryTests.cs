using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using webapi.Applications.GenreOperations.Queries.GetGenreDetail;
using webapi.DBOperations;
using webapi.UnitTests.TestSetup;
using Xunit;

namespace webapi.UnitTests.Applications.GenreOperations.Queries
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mappper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mappper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenGenreIdIsNotinDb_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery Query = new GetGenreDetailQuery(_context, _mappper);
            Query.GenreId = 0;

            FluentActions
                .Invoking(() => Query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Genre is not found!");
        }

        [Fact]
        public void WhenGivenGenreIdIsinDB_InvalidOperationException_shouldBeReturn()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mappper);
            query.GenreId = 2;

            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == query.GenreId);
            genre.Should().NotBeNull();
        }
    }
}
