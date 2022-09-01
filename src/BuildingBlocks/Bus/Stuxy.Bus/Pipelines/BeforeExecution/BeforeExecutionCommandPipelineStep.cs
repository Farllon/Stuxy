using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Pipelines.BeforeExecution
{
    public abstract class BeforeExecutionCommandPipelineStep<TCommand, TResponse> : BeforeExecutionMessagePipelineStep<IBeforeExecutionCommandPipelineStep<TCommand, TResponse>, TCommand, TResponse>, IBeforeExecutionCommandPipelineStep<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        
    }

    public abstract class BeforeExecutionCommandPipelineStep<TCommand> : BeforeExecutionMessagePipelineStep<IBeforeExecutionCommandPipelineStep<TCommand>, TCommand>, IBeforeExecutionCommandPipelineStep<TCommand>
        where TCommand : ICommand
    {
        
    }
}
