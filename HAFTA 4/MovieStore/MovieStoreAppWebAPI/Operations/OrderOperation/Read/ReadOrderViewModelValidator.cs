using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.OrderOperation.Read
{
    public class ReadOrderViewModelValidator : AbstractValidator<ReadOrderViewModel>
    {
        public ReadOrderViewModelValidator()
        {
            RuleFor(x => x.Id).NotEqual(0).WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");

            RuleFor(x => x.User.Id).NotEqual(0).WithMessage("User ID cannot be empty");
            RuleFor(x => x.User.Id).GreaterThan(0).WithMessage("User ID must be greater than 0");
        }
    }
}
