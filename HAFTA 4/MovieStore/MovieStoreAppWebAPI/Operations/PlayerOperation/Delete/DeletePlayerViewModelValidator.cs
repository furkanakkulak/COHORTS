using FluentValidation;

using MovieStoreAppWebAPI.Operations.PlayerOperation.Delete;

namespace MovieStoreAppWebAPI.Operations.PlayerOperation.Create
{
    public class DeletePlayerViewModelValidator : AbstractValidator<DeletePlayerViewModel>
    {
        public DeletePlayerViewModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
