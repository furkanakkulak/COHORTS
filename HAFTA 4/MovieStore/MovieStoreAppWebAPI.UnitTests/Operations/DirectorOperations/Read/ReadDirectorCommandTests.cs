using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Read;
using MovieStoreAppWebAPI.RequestFeatures;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.DirectorOperations.Create
{
    public class ReadDirectorCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public ReadDirectorCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenFetchGetById_ReadDirectorCommand_ShouldBeGiveExactValue()
        {
            ReadDirectorCommand command = new ReadDirectorCommand(_context, _mapper)
            {
                Model = new ReadDirectorViewModel()
                {
                    Id = 1
                }
            };

            var result = command.GetById();

            result.Success.Should().Be(true);
            result.Data.Name.Should().Be("Christopher");
            result.Data.Surname.Should().Be("Nolan");
            result.Data.Id.Should().Be(1);
        }

        [Fact]
        public void WhenFetchGetAllWithoutFilter_ReadDirectorCommand_ShouldBeGiveExactValues()
        {
            ReadDirectorCommand command = new ReadDirectorCommand(_context, _mapper);

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(8);
        }

        [Fact]
        public void WhenFetchGetAllWithNameFilter_ReadDirectorCommand_ShouldBeGiveExactValues()
        {
            ReadDirectorCommand command = new ReadDirectorCommand(_context, _mapper)
            {
                Parameters = new DirectorParameters()
                {
                    Name = "in"
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithSurnameFilter_ReadDirectorCommand_ShouldBeGiveExactValues()
        {
            ReadDirectorCommand command = new ReadDirectorCommand(_context, _mapper)
            {
                Parameters = new DirectorParameters()
                {
                    Surname = "an"
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(3);
        }

        [Fact]
        public void WhenFetchGetAllWithAllFilter_ReadDirectorCommand_ShouldBeGiveExactValues()
        {
            ReadDirectorCommand command = new ReadDirectorCommand(_context, _mapper)
            {
                Parameters = new DirectorParameters()
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
