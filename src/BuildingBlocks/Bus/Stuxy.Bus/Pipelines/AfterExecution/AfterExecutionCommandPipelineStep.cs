using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Pipelines.AfterExecution
{
    public abstract class AfterExecutionCommandPipelineStep<TCommand, TResponse> : AfterExecutionMessagePipelineStep<IAfterExecutionCommandPipelineStep<TCommand, TResponse>, TCommand>, IAfterExecutionCommandPipelineStep<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {

    }

    public abstract class AfterExecutionCommandPipelineStep<TCommand> : AfterExecutionMessagePipelineStep<IAfterExecutionCommandPipelineStep<TCommand>, TCommand>, IAfterExecutionCommandPipelineStep<TCommand>
        where TCommand : ICommand
    {

    }
}
