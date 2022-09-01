using Stuxy.Bus.Communication;
using Stuxy.Bus.Messaging;
using System.Threading.Tasks;
using System.Threading;

namespace Stuxy.Bus.Pipelines.BeforeExecution
{
    public interface IBeforeExecutionMessagePipelineStep<TPipelineStep, TMessage, TResponse>
        where TPipelineStep : IBeforeExecutionMessagePipelineStep<TPipelineStep, TMessage, TResponse>
        where TMessage : IMessage
    {
        TPipelineStep SetNext(TPipelineStep next);

        Task<TResponse> Handle(TMessage message, IServiceBus bus, CancellationToken cancellationToken);
    }

    public interface IBeforeExecutionMessagePipelineStep<TPipelineStep, TMessage>
        where TPipelineStep : IBeforeExecutionMessagePipelineStep<TPipelineStep, TMessage>
        where TMessage : IMessage
    {
        TPipelineStep SetNext(TPipelineStep next);

        Task Handle(TMessage message, IServiceBus bus, CancellationToken cancellationToken);
    }
}
