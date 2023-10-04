using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.GenreOperation.Read;
using MovieStoreAppWebAPI.RequestFeatures;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.GenreOperations.Create
{
    public class ReadGenreCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public ReadGenreCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenFetchGetById_ReadGenreCommand_ShouldBeGiveExactValue()
        {
            ReadGenreCommand command = new ReadGenreCommand(_context, _mapper)
            {
                Model = new ReadGenreViewModel()
                {
                    Id = 1
                }
            };

            var result = command.GetById();

            result.Success.Should().Be(true);
            result.Data.Name.Should().Be("Action");
            result.Data.Id.Should().Be(1);
        }

        [Fact]
        public void WhenFetchGetAllWithoutFilter_ReadGenreCommand_ShouldBeGiveExactValues()
        {
            ReadGenreCommand command = new ReadGenreCommand(_context, _mapper);

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(10);
        }


        [Fact]
        public void WhenFetchAllDataWithNameFilter_ReadGenreCommand_ShouldBeGiveExactValues()
        {
            ReadGenreCommand command = new ReadGenreCommand(_context, _mapper)
            {
                Parameters = new GenreParameters()
                {
                    Name = "en"
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }


    }
}
