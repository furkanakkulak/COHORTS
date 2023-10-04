using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.AuthOperation.Login;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Utilities.Results;

using Newtonsoft.Json.Linq;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MovieStoreAppWebAPI.Operations.AuthOperation.RefreshToken
{
    public class UserRefreshCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public TokenModel Model { get; set; } = new TokenModel();
        public UserRefreshCommand(IMovieStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public DataResult<TokenModel> Handle()
        {
            string? accessToken = Model.AccessToken;
            string? refreshToken = Model.RefreshToken;

            var principal = TokenOperation.TokenHandler.GetPrincipalFromExpiredToken(
                accessToken: accessToken,
                securityKey: _configuration["Token:SecurityKey"]
                );

            if (principal == null)
            {
                throw new ArgumentException(Messages.InvalidTokens);
            }

            string username = principal.Identity.Name;

            User? user = _dbContext.Users.SingleOrDefault(x => x.Username == username);

            if (user == null || user.RefreshToken != refreshToken)
                throw new ArgumentException(Messages.InvalidTokens);

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new ArgumentException(Messages.SessionExpired);

            var tokenExpiration =
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Token:TokenTimeoutMinutes"]));

            var newAccessToken = TokenOperation.TokenHandler.CreateAccessToken(
                username: user.Username,
                securityKey: _configuration["Token:SecurityKey"],
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expiration: tokenExpiration,
                additionalClaims: principal.Claims.ToArray()
                );

            var newRefreshToken = TokenOperation.TokenHandler.CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = tokenExpiration;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            var tokenModel = new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };

            return new SuccessDataResult<TokenModel>(data: tokenModel);
        }
    }
    public class TokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
