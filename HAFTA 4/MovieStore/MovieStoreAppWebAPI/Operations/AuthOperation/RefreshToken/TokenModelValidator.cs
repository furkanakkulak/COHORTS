using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.AuthOperation.RefreshToken
{
    public class TokenModelValidator : AbstractValidator<TokenModel>
    {
        public TokenModelValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty().WithMessage("Access token cannot be empty");

            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Refresh token cannot be empty");
        }
    }
}
