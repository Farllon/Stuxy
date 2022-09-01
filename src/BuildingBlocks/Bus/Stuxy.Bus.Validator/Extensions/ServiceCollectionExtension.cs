using FluentValidation;
using Stuxy.Bus.Pipelines.BeforeExecution;
using Stuxy.Bus.Validator.Pipelines.BeforeExecution;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusValidations(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddTransient(typeof(IBeforeExecutionCommandPipelineStep<,>), typeof(BeforeExecutionCommandValidatorPipelineStep<,>));
            services.AddTransient(typeof(IBeforeExecutionCommandPipelineStep<>), typeof(BeforeExecutionCommandValidatorPipelineStep<>));

            services.AddTransient(typeof(IBeforeExecutionQueryPipelineStep<,>), typeof(BeforeExecutionQueryValidatorPipelineStep<,>));

            services.AddTransient(typeof(IBeforeExecutionEventPipelineStep<>), typeof(BeforeExecutionEventValidatorPipelineStep<>));

            var enumerator = assemblies.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var assembly = enumerator.Current as Assembly;

                var validators = assembly
                    .GetTypes()
                    .Where(t => typeof(IValidator).IsAssignableFrom(t));

                var validatorsEnumerator = validators.GetEnumerator();

                while (validatorsEnumerator.MoveNext())
                {
                    var validator = validatorsEnumerator.Current;

                    var messageType = validator.BaseType;

                    services.AddSingleton(messageType, validator);
                }

                validatorsEnumerator.Dispose();
            }
        }
    }
}
