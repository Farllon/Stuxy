using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Pipelines.AfterExecution
{
    public interface IAfterExecutionCommandPipelineStep<TCommand, TResponse> : IAfterExecutionMessagePipelineStep<IAfterExecutionCommandPipelineStep<TCommand, TResponse>, TCommand>
        where TCommand : ICommand<TResponse>
    {

    }

    public interface IAfterExecutionCommandPipelineStep<TCommand> : IAfterExecutionMessagePipelineStep<IAfterExecutionCommandPipelineStep<TCommand>, TCommand>
        where TCommand : ICommand
    {

    }
}
