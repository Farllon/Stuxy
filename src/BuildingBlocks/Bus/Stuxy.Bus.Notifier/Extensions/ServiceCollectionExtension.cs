using Stuxy.Bus.Notifier;
using Stuxy.Bus.Notifier.Collections;
using Stuxy.Bus.Notifier.Notifications;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusNotifications(this IServiceCollection services)
        {
            services.AddScoped(provider => new BusNotificationCollection<BusWarning>());
            services.AddScoped(provider => new BusNotificationCollection<BusError>());
            services.AddScoped(provider => new BusNotificationCollection<BusSystemError>());
        }
    }
}
