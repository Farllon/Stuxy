using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Pipelines.BeforeExecution
{
    public interface IBeforeExecutionCommandPipelineStep<TCommand, TResponse> : IBeforeExecutionMessagePipelineStep<IBeforeExecutionCommandPipelineStep<TCommand, TResponse>, TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {

    }

    public interface IBeforeExecutionCommandPipelineStep<TCommand> : IBeforeExecutionMessagePipelineStep<IBeforeExecutionCommandPipelineStep<TCommand>, TCommand>
        where TCommand : ICommand
    {

    }
}
