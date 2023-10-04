using AutoMapper;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Services.Encryption;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.UserOperation.Create
{
    public class CreateUserCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateUserViewModel Model { get; set; } = new CreateUserViewModel();

        public CreateUserCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Result Handle()
        {
            List<Genre> genres = CheckIfGenresExists();

            string passwordSalt;
            string passwordHash = PasswordHelper.CreatePasswordHash(
                Model.Password,
                out passwordSalt
            );

            User newUser = _mapper.Map<User>(Model);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.FavouriteGenres = genres;

            Console.WriteLine("Hashed Password: " + passwordHash);
            Console.WriteLine("Salt: " + passwordSalt);

            _dbContext.Users.Add(newUser);

            _dbContext.SaveChanges();

            return new SuccessResult(message: Messages.UserSuccessfullySignedUp);
        }

        private List<Genre> CheckIfGenresExists()
        {
            List<Genre> genres = new List<Genre>();

            foreach (int genreId in Model.FavouriteGenreIds)
            {
                Genre? searchedGenre = _dbContext.Genres.SingleOrDefault(x => x.Id == genreId);

                if (searchedGenre == null)
                    throw new EntityNullException(Messages.GenreDoesNotExist);

                genres.Add(searchedGenre);
            }

            return genres;
        }
    }

    public class CreateUserViewModel
    {
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<int> FavouriteGenreIds { get; set; } = new List<int>();
    }
}
