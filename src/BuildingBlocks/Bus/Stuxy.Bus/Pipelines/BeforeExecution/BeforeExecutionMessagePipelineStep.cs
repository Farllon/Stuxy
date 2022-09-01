using Stuxy.Bus.Communication;
using Stuxy.Bus.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Stuxy.Bus.Pipelines.BeforeExecution
{
    public abstract class BeforeExecutionMessagePipelineStep<TPipelineStep, TMessage, TResponse> : IBeforeExecutionMessagePipelineStep<TPipelineStep, TMessage, TResponse>
        where TPipelineStep : IBeforeExecutionMessagePipelineStep<TPipelineStep, TMessage, TResponse>
        where TMessage : IMessage
    {

        private TPipelineStep _next;

        public virtual Task<TResponse> Handle(TMessage message, IServiceBus bus, CancellationToken cancellationToken)
        {
            if (_next != null)
            {
                return _next.Handle(message, bus, cancellationToken);
            }
            else
            {
                return null;
            }
        }

        public TPipelineStep SetNext(TPipelineStep next)
        {
            _next = next;

            return next;
        }
    }

    public abstract class BeforeExecutionMessagePipelineStep<TPipelineStep, TMessage> : IBeforeExecutionMessagePipelineStep<TPipelineStep, TMessage>
        where TPipelineStep : IBeforeExecutionMessagePipelineStep<TPipelineStep, TMessage>
        where TMessage : IMessage
    {
        private TPipelineStep _next;

        public virtual Task Handle(TMessage message, IServiceBus bus, CancellationToken cancellationToken)
        {
            if (_next != null)
            {
                return _next.Handle(message, bus, cancellationToken);
            }
            else
            {
                return null;
            }
        }

        public TPipelineStep SetNext(TPipelineStep next)
        {
            _next = next;

            return next;
        }
    }
}
