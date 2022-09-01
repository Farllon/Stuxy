using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Pipelines.BeforeExecution
{
    public abstract class BeforeExecutionQueryPipelineStep<TQuery, TResponse> : BeforeExecutionMessagePipelineStep<IBeforeExecutionQueryPipelineStep<TQuery, TResponse>, TQuery, TResponse>, IBeforeExecutionQueryPipelineStep<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        
    }
}
