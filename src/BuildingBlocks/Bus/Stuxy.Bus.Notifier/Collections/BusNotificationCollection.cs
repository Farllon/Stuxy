using System.Collections.Generic;
using Stuxy.Bus.Notifier.Notifications;

namespace Stuxy.Bus.Notifier.Collections
{
    internal class BusNotificationCollection<TBusNotification> : List<TBusNotification>
        where TBusNotification : BusNotification
    {
    }
}
