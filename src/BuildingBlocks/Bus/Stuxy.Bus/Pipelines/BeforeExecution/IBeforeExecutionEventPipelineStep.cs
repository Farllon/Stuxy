using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Pipelines.BeforeExecution
{
    public interface IBeforeExecutionEventPipelineStep<TEvent> : IBeforeExecutionMessagePipelineStep<IBeforeExecutionEventPipelineStep<TEvent>, TEvent>
        where TEvent : IEvent
    {

    }
}
