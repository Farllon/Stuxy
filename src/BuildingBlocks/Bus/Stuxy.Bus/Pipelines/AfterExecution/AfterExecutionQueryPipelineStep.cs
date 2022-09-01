using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Pipelines.AfterExecution
{
    public abstract class AfterExecutionQueryPipelineStep<TQuery, TResponse> : AfterExecutionMessagePipelineStep<IAfterExecutionQueryPipelineStep<TQuery, TResponse>, TQuery>, IAfterExecutionQueryPipelineStep<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
    }
}
