using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.UserOperation.Read
{
    public class ReadUserViewModelValidator : AbstractValidator<ReadUserViewModel>
    {
        public ReadUserViewModelValidator()
        {
            RuleFor(x => x.Id).NotEqual(0).WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
