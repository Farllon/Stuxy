using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Pipelines.AfterExecution
{
    public interface IAfterExecutionQueryPipelineStep<TQuery, TResponse> : IAfterExecutionMessagePipelineStep<IAfterExecutionQueryPipelineStep<TQuery, TResponse>, TQuery>
        where TQuery : IQuery<TResponse>
    {

    }
}
