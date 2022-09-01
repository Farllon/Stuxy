using Stuxy.Bus.Communication;
using Stuxy.Bus.Messaging;
using Stuxy.Bus.Pipelines.BeforeExecution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stuxy.Bus.Pipelines.AfterExecution
{
    public abstract class AfterExecutionMessagePipelineStep<TPipelineStep, TMessage> : IAfterExecutionMessagePipelineStep<TPipelineStep, TMessage>
        where TPipelineStep : IAfterExecutionMessagePipelineStep<TPipelineStep, TMessage>
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
