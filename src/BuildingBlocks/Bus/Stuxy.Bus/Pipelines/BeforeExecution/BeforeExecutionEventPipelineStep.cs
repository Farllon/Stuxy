using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Pipelines.BeforeExecution
{
    public abstract class BeforeExecutionEventPipelineStep<TEvent> : BeforeExecutionMessagePipelineStep<IBeforeExecutionEventPipelineStep<TEvent>, TEvent>, IBeforeExecutionEventPipelineStep<TEvent>
        where TEvent : IEvent
    {
        
    }
}
