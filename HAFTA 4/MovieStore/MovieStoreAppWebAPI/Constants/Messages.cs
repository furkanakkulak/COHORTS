using MovieStoreAppWebAPI.Entities;

using System.Runtime.Serialization;

namespace MovieStoreAppWebAPI.Constants
{
    public static class Messages
    {
        public const string DirectorSuccessfullyCreated = "Director successfully created";
        public const string DirectorAlreadyExists = "Director has already exists";
        public const string DirectorDoesNotExist = "Director does not exist";
        public const string DirectorSuccessfullyDeleted = "Director successfully deleted";
        public const string DirectorSuccessfullyUpdated = "Director successfully updated";

        public const string GenreDoesNotExist = "Genre does not exist";
        public const string GenreSuccessfullyUpdated = "Genre successfully updated";
        public const string GenreSuccessfullyDeleted = "Genre successfully deleted";
        public const string GenreSuccessfullyCreated = "Genre successfully created";
        public const string GenreAlreadyExists = "Genre has already exists";

        public const string FilmDoesNotExist = "Film does not exist";
        public const string FilmAlreadyExists = "Film has already exists";
        public const string FilmSuccessfullyUpdated = "Film successfuly updated";
        public const string FilmSuccessfullyDeleted = "Film successfully deleted";
        public const string FilmSuccessfullyCreated = "Film successfully created";

        public const string PlayerDoesNotExist = "Player does not exist";
        public const string PlayerSuccessfullyDeleted = "Player successfully deleted";
        public const string PlayerSuccessfullyCreated = "Player successfully created";
        public const string PlayerSuccessfullyUpdated = "Player successfully updated";
        public const string PlayerAlreadyExists = "Player has already exists";

        public const string UserDoesNotExist = "User does not exist";
        public const string UserSuccessfullyDeleted = "User successfully deleted";
        public const string UserAlreadyBoughtFilm = "User has already bought this film";
        public const string UserSuccessfullyOrdered = "User successfully ordered";
        public const string OrderDoesNotExist = "Order does not exist";
        public const string OrderSuccessfullyDeleted = "Order successfully deleted";

        public const string InvalidUsernameOrPassword = "Invalid username or password";
        public const string InvalidTokens = "Invalid access token or refresh token";
        public const string SessionExpired = "Session has expired";
        internal static string? UserSuccessfullySignedUp = "You have successfully signed up";
        internal static string? UserSuccessfullyLoggedIn = "You have successfully logged in";
    }
}
