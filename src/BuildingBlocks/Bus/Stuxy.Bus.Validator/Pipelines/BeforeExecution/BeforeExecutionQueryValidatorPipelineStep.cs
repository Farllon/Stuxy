using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Stuxy.Bus.Communication;
using Stuxy.Bus.Messaging;
using Stuxy.Bus.Notifier;
using System;
using System.Threading.Tasks;
using System.Threading;
using Stuxy.Bus.Pipelines.BeforeExecution;
using Stuxy.Bus.Notifier.Notifications;

namespace Stuxy.Bus.Validator.Pipelines.BeforeExecution
{
    internal class BeforeExecutionQueryValidatorPipelineStep<TQuery, TResponse> : BeforeExecutionQueryPipelineStep<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        public override Task<TResponse> Handle(TQuery query, IServiceBus bus, CancellationToken cancellationToken)
        {
            var validator = bus.Context.GetService<AbstractValidator<TQuery>>();

            if (validator != null)
            {
                var validationResult = validator.Validate(query);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                        bus.Notify(new BusError(error.ErrorMessage));

                    return Task.FromResult<TResponse>(default);
                }
            }

            return base.Handle(query, bus, cancellationToken);
        }
    }
}
