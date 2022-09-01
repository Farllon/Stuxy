using Stuxy.Bus.Messaging;
using Stuxy.Identity.Contracts.v1._0.Requests.Accounts;

namespace Stuxy.Identity.Abstractions.Commands.Accounts
{
    public class RegisterAccountCommand : Command
    {
        public RegisterAccountRequest NewAccountInformations { get; }

        public RegisterAccountCommand(RegisterAccountRequest newAccountInformations)
        {
            NewAccountInformations = newAccountInformations;
        }
    }
}
