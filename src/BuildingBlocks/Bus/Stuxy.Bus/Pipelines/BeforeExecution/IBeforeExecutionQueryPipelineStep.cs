using Stuxy.Bus.Messaging;
using System.Threading.Tasks;
using System.Threading;
using Stuxy.Bus.Communication;

namespace Stuxy.Bus.Pipelines.BeforeExecution
{
    public interface IBeforeExecutionQueryPipelineStep<TQuery, TResponse> : IBeforeExecutionMessagePipelineStep<IBeforeExecutionQueryPipelineStep<TQuery, TResponse>, TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {

    }
}
