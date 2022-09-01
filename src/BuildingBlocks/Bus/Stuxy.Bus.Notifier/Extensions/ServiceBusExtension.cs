using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Stuxy.Bus.Notifier.Collections;
using Stuxy.Bus.Notifier.Notifications;

namespace Stuxy.Bus.Communication
{
    public static class ServiceBusExtension
    {
        public static bool HasNotifications(this IServiceBus bus)
            => HasWarnings(bus) || HasErrors(bus) || HasSystemErrors(bus);

        public static bool HasWarnings(this IServiceBus bus)
            => GetWarnings(bus).Any();

        public static bool HasErrors(this IServiceBus bus)
            => GetErrors(bus).Any();

        public static bool HasSystemErrors(this IServiceBus bus)
            => GetSystemErrors(bus).Any();

        public static IEnumerable<BusWarning> GetWarnings(this IServiceBus bus)
            => bus.Context.GetRequiredService<BusNotificationCollection<BusWarning>>();

        public static IEnumerable<BusError> GetErrors(this IServiceBus bus)
            => bus.Context.GetRequiredService<BusNotificationCollection<BusError>>();

        public static IEnumerable<BusSystemError> GetSystemErrors(this IServiceBus bus)
            => bus.Context.GetRequiredService<BusNotificationCollection<BusSystemError>>();

        public static void Notify<TBusNotification>(this IServiceBus bus, TBusNotification notification)
            where TBusNotification : BusNotification
        {
            if (notification is BusWarning warning)
                bus.Context.GetRequiredService<BusNotificationCollection<BusWarning>>().Add(warning);
            else if (notification is BusError error)
                bus.Context.GetRequiredService<BusNotificationCollection<BusError>>().Add(error);
            else if (notification is BusSystemError systemError)
                bus.Context.GetRequiredService<BusNotificationCollection<BusSystemError>>().Add(systemError);
        }
    }
}
