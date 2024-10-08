using FluentValidation;
using FluentValidation.Results;
using User_Service.API.Interfaces.Services;

namespace User_Service.API.Services
{
    public abstract class Service(INotifier notifier)
    {
        private readonly INotifier _notifier = notifier;

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }
        protected void Notify(string errorMessage) => _notifier.Handle(new(errorMessage));
        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : class
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
                return true;

            Notify(validator);

            return false;
        }
    }
}
