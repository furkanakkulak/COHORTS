using FluentValidation;

namespace MovieStoreAppWebAPI.Operations.DirectorOperation.Update
{
    public class UpdateDirectorViewModelValidator : AbstractValidator<UpdateDirectorViewModel>
    {
        public UpdateDirectorViewModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Minimum length of name must be 3");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Maximum length of name must be 50");

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname cannot be empty");
            RuleFor(x => x.Surname).MinimumLength(3).WithMessage("Minimum length of surname must be 3");
            RuleFor(x => x.Surname).MaximumLength(50).WithMessage("Maximum length of surname must be 50");
        }
    }
}
