using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace metrics_humidity
{
    public static class AppBuilderExtensions
    {
        public static void RegisterMetrics(this IApplicationBuilder app)
        {
            var summaryHum = Metrics.CreateGauge("room_humidity", "The humidity in the room");
            var summaryTemp = Metrics.CreateGauge("room_temp", "The temperature in the room");

            var serviceProvider = app.ApplicationServices;
            var deconzClient = serviceProvider.GetService<IDeconzApiClient>();

            Metrics.DefaultRegistry.AddBeforeCollectCallback(async (cancel) =>
            {
                var res = await deconzClient.GetLatestSensors(cancel);
                if (res != null)
                {
                    summaryHum.Set(res.Humidity);
                    summaryTemp.Set(res.Temperature);
                }
            });
        }
    }
}
