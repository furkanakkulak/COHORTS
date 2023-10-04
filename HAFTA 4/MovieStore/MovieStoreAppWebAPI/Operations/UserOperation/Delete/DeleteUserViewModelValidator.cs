using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.UserOperation.Delete
{
    public class DeleteUserViewModelValidator : AbstractValidator<DeleteUserViewModel>
    {
        public DeleteUserViewModelValidator()
        {
            RuleFor(x => x.Id).NotEqual(0).WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
