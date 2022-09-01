using Stuxy.Bus.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stuxy.Bus.Pipelines.AfterExecution
{
    public interface IAfterExecutionEventPipelineStep<TEvent> : IAfterExecutionMessagePipelineStep<IAfterExecutionEventPipelineStep<TEvent>, TEvent>
        where TEvent : IEvent
    {
    }
}
