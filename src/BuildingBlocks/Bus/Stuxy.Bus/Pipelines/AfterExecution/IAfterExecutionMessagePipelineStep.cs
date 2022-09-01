using Stuxy.Bus.Communication;
using Stuxy.Bus.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Stuxy.Bus.Pipelines.AfterExecution
{
    public interface IAfterExecutionMessagePipelineStep<TPipelineStep, TMessage>
        where TPipelineStep : IAfterExecutionMessagePipelineStep<TPipelineStep, TMessage>
        where TMessage : IMessage
    {
        TPipelineStep SetNext(TPipelineStep next);

        Task Handle(TMessage message, IServiceBus bus, CancellationToken cancellationToken);
    }
}
