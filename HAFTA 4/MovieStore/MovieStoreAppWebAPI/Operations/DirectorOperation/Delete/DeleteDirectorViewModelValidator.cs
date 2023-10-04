using FluentValidation;

using MovieStoreAppWebAPI.Operations.DirectorOperation.Delete;

namespace MovieStoreAppWebAPI.Operations.DirectorOperation.Create
{
    public class DeleteDirectorViewModelValidator : AbstractValidator<DeleteDirectorViewModel>
    {
        public DeleteDirectorViewModelValidator()
        {
            RuleFor(x => x.Id).NotEqual(0).WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
