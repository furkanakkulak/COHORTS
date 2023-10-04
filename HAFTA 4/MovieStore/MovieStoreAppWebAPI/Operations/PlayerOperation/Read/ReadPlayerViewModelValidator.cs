using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.PlayerOperation.Read
{
    public class ReadPlayerViewModelValidator : AbstractValidator<ReadPlayerViewModel>
    {
        public ReadPlayerViewModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
