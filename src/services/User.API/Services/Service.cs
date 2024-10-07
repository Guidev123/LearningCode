using FluentValidation;
using FluentValidation.Results;
using User.API.Events.Notify;
using User.API.Interfaces.Events;

namespace User.API.Services
{
    public abstract class Service(INotifyer notifyer)
    {
        private readonly INotifyer _notifyer = notifyer;

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }
        protected void Notify(string errorMessage) => _notifyer.Handle(new(errorMessage));
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
