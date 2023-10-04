using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.OrderOperation.Create
{
    public class CreateOrderViewModelValidator : AbstractValidator<CreateOrderViewModel>
    {
        public CreateOrderViewModelValidator()
        {
            RuleFor(x => x.UserUsername).NotEmpty().WithMessage("User username cannot be empty");

            RuleFor(x => x.FilmId).NotEmpty().WithMessage("Film ID cannot be empty");
            RuleFor(x => x.FilmId).GreaterThan(0).WithMessage("Film ID must be greater than 0");
        }
    }
}
