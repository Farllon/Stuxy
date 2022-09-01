using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Pipelines.AfterExecution
{
    public abstract class AfterExecutionEventPipelineStep<TEvent> : AfterExecutionMessagePipelineStep<IAfterExecutionEventPipelineStep<TEvent>, TEvent>, IAfterExecutionEventPipelineStep<TEvent>
        where TEvent : IEvent
    {

    }
}
