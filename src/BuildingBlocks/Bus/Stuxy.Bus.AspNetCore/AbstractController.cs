using Microsoft.Extensions.DependencyInjection;
using Stuxy.Bus.AspNetCore;
using Stuxy.Bus.Communication;
using System;
using System.Linq;

namespace Microsoft.AspNetCore.Mvc
{
    public class AbstractController : ControllerBase
    {
        protected readonly IServiceBus _bus;

        protected AbstractController(IServiceProvider provider)
        {
            _bus = provider.GetRequiredService<IServiceBus>();
        }

        protected BusResponse<TData> GetResponse<TData>(TData data)
        {
            var warnings = _bus.GetWarnings();
            var errors = _bus.GetErrors();

            if (errors.Any())
                return BusResponse<TData>.CreateFailure(errors, data, warnings);

            return BusResponse<TData>.CreateSuccess(data, warnings);
        }

        protected DetailedBusResponse<TData> GetDetailedResponse<TData>(TData data)
        {
            var warnings = _bus.GetWarnings();
            var errors = _bus.GetErrors();
            var systemErrors = _bus.GetSystemErrors();

            if (errors.Any())
                return (DetailedBusResponse<TData>)DetailedBusResponse<TData>.CreateFailure(errors, data, warnings);

            if (systemErrors.Any())
                return DetailedBusResponse<TData>.CreateInternalFailure(systemErrors, data, warnings, errors);

            return (DetailedBusResponse<TData>)DetailedBusResponse<TData>.CreateSuccess(data, warnings);
        }
    }
}
