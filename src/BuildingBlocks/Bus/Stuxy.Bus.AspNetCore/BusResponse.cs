using Stuxy.Bus.Notifier.Notifications;
using System.Collections.Generic;

namespace Stuxy.Bus.AspNetCore
{
    public class BusResponse<TData>
    {
        public bool Success { get; set; }

        public TData Data { get; set; }

        public IEnumerable<BusWarning> Warnings { get; set; } = new List<BusWarning>();

        public IEnumerable<BusError> Errors { get; set; } = new List<BusError>();

        public static BusResponse<TData> CreateSuccess(TData data, IEnumerable<BusWarning>? warnings = null)
            => new BusResponse<TData> { Success = true, Data = data, Warnings = warnings ?? new List<BusWarning>() };

        public static BusResponse<TData> CreateFailure(IEnumerable<BusError> errors, TData data = default, IEnumerable<BusWarning>? warnings = null)
            => new BusResponse<TData> { Success = false, Data = data, Errors = errors, Warnings = warnings ?? new List<BusWarning>() };
    }

    public class DetailedBusResponse<TData> : BusResponse<TData>
    {
        public IEnumerable<BusSystemError> InternalErrors { get; set; } = new List<BusSystemError>();

        public static DetailedBusResponse<TData> CreateInternalFailure(IEnumerable<BusSystemError> systemErrors, TData data = default, IEnumerable<BusWarning>? warnings = null, IEnumerable<BusError>? errors = null)
            => new DetailedBusResponse<TData> { Success = false, Data = data, Errors = errors ?? new List<BusError>(), Warnings = warnings ?? new List<BusWarning>(), InternalErrors = systemErrors };
    }
}
