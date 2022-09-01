using System;
using System.Collections.Generic;
using System.Text;

namespace Stuxy.Bus.Messaging
{
    public abstract class Command<TResponse> : Message, ICommand<TResponse>
    {
    }

    public abstract class Command : Message, ICommand
    {

    }
}
