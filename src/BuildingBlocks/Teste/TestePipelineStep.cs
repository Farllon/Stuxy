using Stuxy.Bus.Communication;
using Stuxy.Bus.Messaging;
using Stuxy.Bus.Pipelines.BeforeExecution;

namespace Teste
{
    public class TestePipelineStep<TCommand, TResponse> : BeforeExecutionCommandPipelineStep<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public override async Task<TResponse> Handle(TCommand command, IServiceBus bus, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine("Alou");

            return await base.Handle(command, bus, cancellationToken);
        }
    }

    public class TestePipelineStep<TCommand> : BeforeExecutionCommandPipelineStep<TCommand>
        where TCommand : ICommand
    {
        public override async Task Handle(TCommand command, IServiceBus bus, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine("Alou");

            await base.Handle(command, bus, cancellationToken);
        }
    }
}
