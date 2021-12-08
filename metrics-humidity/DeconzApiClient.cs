using Serilog;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace metrics_humidity
{
    public record SensorsData(int Temperature, int Humidity);

    public class DeconzApiClient : IDeconzApiClient
    {
        private record State(int Temperature, int Humidity);
        private record DeconzResult(State State);

        private readonly string _apiUrl;
        private readonly ILogger _logger;
        private readonly string _apiKey;

        private const int TempSensorId = 2;
        private const int HumiditySensorId = 3;

        public DeconzApiClient(string apiUrl, string apiKey, ILogger logger)
        {
            _apiKey = apiKey;
            _logger = logger;
            _apiUrl = apiUrl;
        }

        public async Task<SensorsData> GetLatestSensors(CancellationToken cancel)
        {
            using var client = new HttpClient();

            var jsonOpt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            try
            {
                var tempData = await client.GetFromJsonAsync<DeconzResult>($"{_apiUrl}/{_apiKey}/sensors/{TempSensorId}", jsonOpt, cancel);
                var humidityData = await client.GetFromJsonAsync<DeconzResult>($"{_apiUrl}/{_apiKey}/sensors/{HumiditySensorId}", jsonOpt, cancel);
                _logger.Information<DeconzResult>("Deconz response temp", tempData);
                _logger.Information<DeconzResult>("Deconz response humidity", humidityData);

                return new SensorsData(tempData.State.Temperature / 100, humidityData.State.Humidity / 100);
            }
            catch (Exception e)
            {
                _logger.Error(e, "DeconzApiClient.GetLatestSensors");
                return null;
            }
        }
    }
}
