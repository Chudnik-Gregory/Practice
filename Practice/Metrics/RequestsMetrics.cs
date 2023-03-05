using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Formatters.Json;

namespace Practice.Metrics
{
    public static class RequestsMetrics
    {
        public static CounterOptions GetMetrics => new CounterOptions()
        {
            Context = "Requests",
            Name = "123",
            MeasurementUnit = Unit.Calls
        };
    }
}
