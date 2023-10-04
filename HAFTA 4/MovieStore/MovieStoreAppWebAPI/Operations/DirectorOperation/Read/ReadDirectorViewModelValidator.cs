using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.DirectorOperation.Read
{
    public class ReadDirectorViewModelValidator : AbstractValidator<ReadDirectorViewModel>
    {
        public ReadDirectorViewModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
