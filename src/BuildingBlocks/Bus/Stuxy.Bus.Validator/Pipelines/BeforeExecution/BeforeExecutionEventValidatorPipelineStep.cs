using FluentValidation;
using Stuxy.Bus.Communication;
using Stuxy.Bus.Messaging;
using Stuxy.Bus.Notifier.Collections;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.DependencyInjection;
using Stuxy.Bus.Pipelines.BeforeExecution;
using Stuxy.Bus.Notifier.Notifications;

namespace Stuxy.Bus.Validator.Pipelines.BeforeExecution
{
    internal class BeforeExecutionEventValidatorPipelineStep<TEvent> : BeforeExecutionEventPipelineStep<TEvent>
        where TEvent : IEvent
    {
        public override Task Handle(TEvent @event, IServiceBus bus, CancellationToken cancellationToken)
        {
            var validator = bus.Context.GetService<AbstractValidator<TEvent>>();

            if (validator != null)
            {
                var validationResult = validator.Validate(@event);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                        bus.Notify(new BusError(error.ErrorMessage));

                    return Task.CompletedTask;
                }
            }

            return base.Handle(@event, bus, cancellationToken);
        }
    }
}
