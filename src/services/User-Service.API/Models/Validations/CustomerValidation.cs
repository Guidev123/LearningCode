using FluentValidation;
using User_Service.API.Models;

namespace User_Service.API.Models.Validations
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(f => f.FullName)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("A senha é obrigatória.")
               .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
               .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
               .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
               .Matches(@"\d").WithMessage("A senha deve conter pelo menos um dígito.")
               .Matches(@"[\W_]").WithMessage("A senha deve conter pelo menos um caractere especial.");

            RuleFor(x => x.Email.Address)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("É necessário um endereço de e-mail válido.");

            RuleFor(f => f.Document.Number.Length)
                .Equal(11)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

        }
    }
}
