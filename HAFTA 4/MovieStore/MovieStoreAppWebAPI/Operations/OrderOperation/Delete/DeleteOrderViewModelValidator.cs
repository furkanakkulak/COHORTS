using FluentValidation;

using MovieStoreAppWebAPI.Operations.OrderOperation.Delete;

namespace MovieStoreAppWebAPI.Operations.OrderOperation.Create
{
    public class DeleteOrderViewModelValidator : AbstractValidator<DeleteOrderViewModel>
    {
        public DeleteOrderViewModelValidator()
        {
            RuleFor(x => x.Id).NotEqual(0).WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
