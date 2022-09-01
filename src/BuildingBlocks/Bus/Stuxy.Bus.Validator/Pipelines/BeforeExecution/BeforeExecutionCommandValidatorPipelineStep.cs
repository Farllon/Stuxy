using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Stuxy.Bus.Communication;
using Stuxy.Bus.Messaging;
using Stuxy.Bus.Notifier;
using Stuxy.Bus.Notifier.Notifications;
using Stuxy.Bus.Pipelines.BeforeExecution;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stuxy.Bus.Validator.Pipelines.BeforeExecution
{
    internal class BeforeExecutionCommandValidatorPipelineStep<TCommand> : BeforeExecutionCommandPipelineStep<TCommand>
        where TCommand : ICommand
    {
        public override Task Handle(TCommand command, IServiceBus bus, CancellationToken cancellationToken)
        {
            var validator = bus.Context.GetService<AbstractValidator<TCommand>>();

            if (validator != null)
            {
                var validationResult = validator.Validate(command);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                        bus.Notify(new BusError(error.ErrorMessage));

                    return Task.CompletedTask;
                }
            }

            return base.Handle(command, bus, cancellationToken);
        }
    }

    internal class BeforeExecutionCommandValidatorPipelineStep<TCommand, TResponse> : BeforeExecutionCommandPipelineStep<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public override Task<TResponse> Handle(TCommand command, IServiceBus bus, CancellationToken cancellationToken)
        {
            var validator = bus.Context.GetService<AbstractValidator<TCommand>>();

            if (validator != null)
            {
                var validationResult = validator.Validate(command);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                        bus.Notify(new BusError(error.ErrorMessage));

                    return Task.FromResult<TResponse>(default);
                }
            }

            return base.Handle(command, bus, cancellationToken);
        }
    }
}
