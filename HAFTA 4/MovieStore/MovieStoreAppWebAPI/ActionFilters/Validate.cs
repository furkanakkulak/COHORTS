using FluentValidation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using MovieStoreAppWebAPI.Services.Validation;

namespace MovieStoreAppWebAPI.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false)]
    public class Validate : ActionFilterAttribute
    {
        private IValidator _validator;
        public Validate(Type validatorType)
        {
            if (!(validatorType).GetInterfaces().Contains(typeof(IValidator)))
                throw new ArgumentException("Wrong validator type");

            _validator = (IValidator)Activator.CreateInstance(validatorType);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
         {
            var bodyToBeValidated = context.ActionArguments.SingleOrDefault(aa => aa.Value.GetType().Name.Contains("ViewModel")).Value;
            if(bodyToBeValidated != null)
            {
                ValidationTool.Validate(_validator,bodyToBeValidated);
            }

            base.OnActionExecuting(context);
        }
    }
}
