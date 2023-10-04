using Microsoft.EntityFrameworkCore;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.AuthOperation.RefreshToken;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Services.Encryption;
using MovieStoreAppWebAPI.Utilities.Results;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

namespace MovieStoreAppWebAPI.Operations.AuthOperation.Login
{
    public class UserLoginCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public UserLoginViewModel Model { get; set; } = new UserLoginViewModel();
        public UserLoginCommand(IMovieStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public DataResult<TokenModel> Handle()
        {
            User user = CheckIfUserExists();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var tokenExpiration = 
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Token:TokenTimeoutMinutes"]));

            var token = TokenOperation.TokenHandler.CreateAccessToken(
                username:user.Username,
                securityKey:_configuration["Token:SecurityKey"],
                issuer:_configuration["Token:Issuer"],
                audience:_configuration["Token:Audience"],
                expiration: tokenExpiration,
                additionalClaims : claims.ToArray()    
                );

            var newRefreshToken = TokenOperation.TokenHandler.CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = tokenExpiration;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            var tokenModel = new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = newRefreshToken
            };

            return new SuccessDataResult<TokenModel>(data: tokenModel,message:Messages.UserSuccessfullyLoggedIn);
        }

        private User CheckIfUserExists()
        {

            User? searchedUser = _dbContext.Users
                .Include(x => x.Roles)
                .SingleOrDefault(c => c.Username == Model.Username);

            if (searchedUser == null)
                throw new EntityNullException(Messages.UserDoesNotExist);

            bool verifyResult = PasswordHelper.VerifyPassword(Model.Password, searchedUser.PasswordHash, searchedUser.PasswordSalt);
            if (!verifyResult)
                throw new ArgumentException(Messages.InvalidUsernameOrPassword);

            return searchedUser;
        }

    }

    public class UserLoginViewModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

}
