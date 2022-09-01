using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Stuxy.Bus.Messaging;
using Stuxy.Bus.Pipelines.BeforeExecution;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stuxy.Bus.Communication
{
    public class ServiceBus : IServiceBus
    {
        private readonly IMediator _mediator;

        public IServiceProvider Context { get; }

        public ServiceBus(
            IMediator mediator,
            IServiceProvider provider)
        {
            _mediator = mediator;

            Context = provider;
        }

        public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
        {
            var pipelineSteps = Context.GetServices<IBeforeExecutionCommandPipelineStep<TCommand>>();

            var enumerator = pipelineSteps.GetEnumerator();

            IBeforeExecutionCommandPipelineStep<TCommand> first = null;
            IBeforeExecutionCommandPipelineStep<TCommand> last = null;

            while (enumerator.MoveNext())
            {
                var step = enumerator.Current;

                if (first == null)
                {
                    first = step;
                    last = step;
                }
                else
                    last = last.SetNext(step);
            }

            enumerator.Dispose();

            if (last is null)
            {
                await new LastCommandPipelineStep<TCommand>(_mediator).Handle(command, this, cancellationToken);

                return;
            }

            last.SetNext(new LastCommandPipelineStep<TCommand>(_mediator));

            await first.Handle(command, this, cancellationToken);
        }

        public async Task<TResponse> SendAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>
        {
            var pipelineSteps = Context.GetServices<IBeforeExecutionCommandPipelineStep<TCommand, TResponse>>();

            var enumerator = pipelineSteps.GetEnumerator();

            IBeforeExecutionCommandPipelineStep<TCommand, TResponse> first = null;
            IBeforeExecutionCommandPipelineStep<TCommand, TResponse> last = null;

            while (enumerator.MoveNext())
            {
                var step = enumerator.Current;

                if (first == null)
                {
                    first = step;
                    last = step;
                }
                else
                    last = last.SetNext(step);
            }

            enumerator.Dispose();

            if (last is null)
                return await new LastCommandPipelineStep<TCommand, TResponse>(_mediator).Handle(command, this, cancellationToken);

            last.SetNext(new LastCommandPipelineStep<TCommand, TResponse>(_mediator));

            return await first.Handle(command, this, cancellationToken);

        }

        public async Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResponse>
        {
            var pipelineSteps = Context.GetServices<IBeforeExecutionQueryPipelineStep<TQuery, TResponse>>();

            var enumerator = pipelineSteps.GetEnumerator();

            IBeforeExecutionQueryPipelineStep<TQuery, TResponse> first = null;
            IBeforeExecutionQueryPipelineStep<TQuery, TResponse> last = null;

            while (enumerator.MoveNext())
            {
                var step = enumerator.Current;

                if (first == null)
                {
                    first = step;
                    last = step;
                }
                else
                    last = last.SetNext(step);
            }

            enumerator.Dispose();

            if (last is null)
                return await new LastQueryPipelineStep<TQuery, TResponse>(_mediator).Handle(query, this, cancellationToken);

            last.SetNext(new LastQueryPipelineStep<TQuery, TResponse>(_mediator));

            return await first.Handle(query, this, cancellationToken);
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : Event
        {
            var pipelineSteps = Context.GetServices<IBeforeExecutionEventPipelineStep<TEvent>>();

            var enumerator = pipelineSteps.GetEnumerator();

            IBeforeExecutionEventPipelineStep<TEvent> first = null;
            IBeforeExecutionEventPipelineStep<TEvent> last = null;

            while (enumerator.MoveNext())
            {
                var step = enumerator.Current;

                if (first == null)
                {
                    first = step;
                    last = step;
                }
                else
                    last = last.SetNext(step);
            }

            enumerator.Dispose();

            if (last is null)
            {
                await new LastEventPipelineStep<TEvent>(_mediator).Handle(@event, this, cancellationToken);

                return;
            }

            last.SetNext(new LastEventPipelineStep<TEvent>(_mediator));

            await first.Handle(@event, this, cancellationToken);
        }

        private class LastCommandPipelineStep<TCommand> : BeforeExecutionCommandPipelineStep<TCommand>
            where TCommand : ICommand
        {
            private IMediator _mediator;

            public LastCommandPipelineStep(IMediator mediator)
            {
                _mediator = mediator;
            }

            public override Task Handle(TCommand command, IServiceBus bus, CancellationToken cancellationToken)
                => _mediator.Send(command, cancellationToken);
        }

        private class LastCommandPipelineStep<TCommand, TResponse> : BeforeExecutionCommandPipelineStep<TCommand, TResponse>
            where TCommand : ICommand<TResponse>
        {
            private IMediator _mediator;

            public LastCommandPipelineStep(IMediator mediator)
            {
                _mediator = mediator;
            }

            public override Task<TResponse> Handle(TCommand command, IServiceBus bus, CancellationToken cancellationToken)
                => _mediator.Send(command, cancellationToken);
        }

        private class LastQueryPipelineStep<TQuery, TResponse> : BeforeExecutionQueryPipelineStep<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            private IMediator _mediator;

            public LastQueryPipelineStep(IMediator mediator)
            {
                _mediator = mediator;
            }

            public override Task<TResponse> Handle(TQuery query, IServiceBus bus, CancellationToken cancellationToken)
                => _mediator.Send(query, cancellationToken);
        }

        private class LastEventPipelineStep<TEvent> : BeforeExecutionEventPipelineStep<TEvent>
            where TEvent : IEvent
        {
            private IMediator _mediator;

            public LastEventPipelineStep(IMediator mediator)
            {
                _mediator = mediator;
            }

            public override Task Handle(TEvent @event, IServiceBus bus, CancellationToken cancellationToken)
                => _mediator.Send(@event, cancellationToken);
        }
    }
}
