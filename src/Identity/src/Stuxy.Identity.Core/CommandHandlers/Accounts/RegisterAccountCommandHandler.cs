using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Stuxy.Bus.Communication;
using Stuxy.Bus.Handlers;
using Stuxy.Bus.Notifier.Notifications;
using Stuxy.Identity.Abstractions.Commands.Accounts;
using Stuxy.Identity.Core.Models;

namespace Stuxy.Identity.Core.CommandHandlers.Accounts
{
    public class RegisterAccountCommandHandler : ICommandHandler<RegisterAccountCommand>
    {
        private readonly IServiceBus _bus;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterAccountCommandHandler(IServiceProvider provider)
        {
            _bus = provider.GetRequiredService<IServiceBus>();
            _userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        public async Task<Unit> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            var userInformations = request.NewAccountInformations;

            var user = new ApplicationUser
            {
                Name = userInformations.Name,
                UserName = userInformations.UserName,
                NormalizedUserName = userInformations.UserName.ToUpper(),
                Email = userInformations.Email,
                NormalizedEmail = userInformations.Email.ToUpper(),
                PhoneNumber = userInformations.PhoneNumber
            };

            var createUserResult = await _userManager.CreateAsync(user, userInformations.Password);

            if (createUserResult.Succeeded)
                return Unit.Value;

            var enumerator = createUserResult.Errors.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var error = new BusError(enumerator.Current.Description);

                _bus.Notify(error);
            }

            enumerator.Dispose();

            return Unit.Value;
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
