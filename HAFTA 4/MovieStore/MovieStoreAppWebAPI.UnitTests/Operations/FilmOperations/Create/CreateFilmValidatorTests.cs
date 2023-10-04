using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.FilmOperation.Create;

namespace MovieStoreAppWebAPI.UnitTests.Operations.FilmOperations.Create
{
    public class CreateFilmValidatorTests
    {

        [InlineData(null,null,0, "2018-01-01", 0,0,null)]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name,string about, decimal price, string publishedDate,int genreId,int directorId, List<int> playerIds)
        {
            CreateFilmViewModelValidator validator = new CreateFilmViewModelValidator();

            var expectedDate = DateTime.Parse(publishedDate);

            ValidationResult result = validator.Validate(new CreateFilmViewModel()
            {
                Name = name,
                About = about,
                Price = price,
                PublishedDate = expectedDate,
                GenreId = genreId,
                DirectorId = directorId,
                PlayerIds = playerIds
            });

            result.Errors.Should().NotBeNull();
        }
    }
}
