using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Read;
using MovieStoreAppWebAPI.RequestFeatures;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.PlayerOperations.Create
{
    public class ReadPlayerCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public ReadPlayerCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenFetchGetById_ReadPlayerCommand_ShouldBeGiveExactValue()
        {
            ReadPlayerCommand command = new ReadPlayerCommand(_context, _mapper)
            {
                Model = new ReadPlayerViewModel()
                {
                    Id = 1
                }
            };

            var result = command.GetById();

            result.Success.Should().Be(true);
            result.Data.Name.Should().Be("Elizabeth");
            result.Data.Surname.Should().Be("Olsen");
            result.Data.Id.Should().Be(1);
        }

        [Fact]
        public void WhenFetchGetAllWithoutFilter_ReadPlayerCommand_ShouldBeGiveExactValues()
        {
            ReadPlayerCommand command = new ReadPlayerCommand(_context, _mapper);

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(8);
        }

        [Fact]
        public void WhenFetchGetAllWithNameFilter_ReadPlayerCommand_ShouldBeGiveExactValues()
        {
            ReadPlayerCommand command = new ReadPlayerCommand(_context, _mapper)
            {
                Parameters = new PlayerParameters()
                {
                    Name = "an"
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(4);
        }

        [Fact]
        public void WhenFetchGetAllWithSurnameFilter_ReadPlayerCommand_ShouldBeGiveExactValues()
        {
            ReadPlayerCommand command = new ReadPlayerCommand(_context, _mapper)
            {
                Parameters = new PlayerParameters()
                {
                    Surname = "an"
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(3);
        }

        [Fact]
        public void WhenFetchGetAllWithAllFilter_ReadPlayerCommand_ShouldBeGiveExactValues()
        {
            ReadPlayerCommand command = new ReadPlayerCommand(_context, _mapper)
            {
                Parameters = new PlayerParameters()
                {
                    Name = "an",
                    Surname = "an"
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(1);
        }


    }
}
