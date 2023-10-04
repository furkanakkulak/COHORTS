using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.FilmOperation.Read;
using MovieStoreAppWebAPI.RequestFeatures;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.FilmOperations.Create
{
    public class ReadFilmCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public ReadFilmCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenFetchGetById_ReadFilmCommand_ShouldBeGiveExactValue()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Model = new ReadFilmViewModel()
                {
                    Id = 1
                }
            };

            var result = command.GetById();

            result.Success.Should().Be(true);
            result.Data.Name.Should().Be("Interstellar");
            result.Data.Id.Should().Be(1);
        }

        [Fact]
        public void WhenFetchGetAllWithoutFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper);

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(5);
        }

        [Fact]
        public void WhenFetchGetAllWithNameFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    Name = "te"
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithNameAndGenreIdFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    Name = "te",
                    GenreId = 1
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithNameAndDirectorIdFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    Name = "te",
                    DirectorId = 1
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithNameAndOnePlayerIdsFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    Name = "te",
                    PlayerIds = new List<int>() { 1 }
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(1);
        }

        [Fact]
        public void WhenFetchGetAllWithNameAndTwoPlayerIdsFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    Name = "te",
                    PlayerIds = new List<int>() { 1,2 }
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(1);
        }

        [Fact]
        public void WhenFetchGetAllWithNameAndMinimumPublishedDateFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    Name = "te",
                    MinimumPublishedDate = new DateTime(2016,01,01)
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(1);
        }

        [Fact]
        public void WhenFetchGetAllWithNameAndMaximumPublishedDateFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    Name = "te",
                    MaximumPublishedDate = new DateTime(2015, 01, 01)
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(0);
        }

        [Fact]
        public void WhenFetchGetAllWithNameAndMaximumAndMinumPublishedDateFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    Name = "te",
                    MinimumPublishedDate = new DateTime(2015, 01, 01),
                    MaximumPublishedDate = new DateTime(2017, 01, 01)
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithGenreIdFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    GenreId = 2
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithGenreAndDirectorIdFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    DirectorId = 2,
                    GenreId = 2
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithGenreIdAndMinimumPublishedDateFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    MinimumPublishedDate = new DateTime(2017, 01, 01),
                    GenreId = 2
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithGenreIdAndMaximumPublishedDateFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    MaximumPublishedDate = new DateTime(2018, 01, 01),
                    GenreId = 2
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(1);
        }

        [Fact]
        public void WhenFetchGetAllWithGenreIdAndMaximumAndMinimumPublishedDateFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    MinimumPublishedDate = new DateTime(2016, 01, 01),
                    MaximumPublishedDate = new DateTime(2019, 01, 01),
                    GenreId = 2
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithOnePlayerIdsFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    PlayerIds = new List<int>() { 1 }
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(3);
        }

        [Fact]
        public void WhenFetchGetAllWithTwoPlayerIdsFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    PlayerIds = new List<int>() { 1,2 }
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithDirectorIdFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    DirectorId = 2
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithMaximumPublishedDateFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    MaximumPublishedDate = new DateTime(2017,01,01)
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithMinimumPublishedDateFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    MinimumPublishedDate = new DateTime(2017, 01, 01)
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(3);
        }

        [Fact]
        public void WhenFetchGetAllWithMinimumAndMaximumPublishedDateFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    MaximumPublishedDate = new DateTime(2019, 01, 01),
                    MinimumPublishedDate = new DateTime(2017, 01, 01)
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(2);
        }

        [Fact]
        public void WhenFetchGetAllWithAllFilter_ReadFilmCommand_ShouldBeGiveExactValues()
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = new FilmParameters()
                {
                    Name = "te",
                    GenreId = 1,
                    DirectorId = 1,
                    MaximumPublishedDate = new DateTime(2016, 01, 01),
                    MinimumPublishedDate = new DateTime(2015, 01, 01)
                }
            };

            var result = command.GetAll();

            result.Success.Should().Be(true);
            result.Data.Count.Should().Be(1);
        }


    }
}
