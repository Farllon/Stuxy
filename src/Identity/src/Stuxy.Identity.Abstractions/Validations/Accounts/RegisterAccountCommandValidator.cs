using FluentValidation;

using Stuxy.Identity.Abstractions.Commands.Accounts;
using System.Linq;

namespace Stuxy.Identity.Abstractions.Validations.Accounts
{
    public class RegisterAccountCommandValidator : AbstractValidator<RegisterAccountCommand>
    {
        public RegisterAccountCommandValidator()
        {
            RuleFor(command => command.NewAccountInformations.Email)
                .EmailAddress();

            RuleFor(command => command.NewAccountInformations.PhoneNumber)
                .Length(11);

            RuleFor(command => command.NewAccountInformations.UserName)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(command => command.NewAccountInformations.Name)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(command => command.NewAccountInformations.Password)
                .NotEmpty()
                .MaximumLength(48);

            RuleFor(command => command.NewAccountInformations.ConfirmPassword)
                .Equal(command => command.NewAccountInformations.Password);
        }
    }
}
