using FluentAssertions;

using FluentValidation.Results;

using MovieStoreAppWebAPI.Operations.FilmOperation.Update;

namespace MovieStoreAppWebAPI.UnitTests.Operations.FilmOperations.Update
{
    public class UpdateFilmValidatorTests
    {

        [InlineData(0,null,null,0, "2018-01-01", 0,0,null)]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int id,string name,string about, decimal price, string publishedDate,int genreId,int directorId, List<int> playerIds)
        {
            UpdateFilmViewModelValidator validator = new UpdateFilmViewModelValidator();

            var expectedDate = DateTime.Parse(publishedDate);

            ValidationResult result = validator.Validate(new UpdateFilmViewModel()
            {
                Id = id,
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
