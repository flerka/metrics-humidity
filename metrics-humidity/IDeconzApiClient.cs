using System.Threading;
using System.Threading.Tasks;

namespace metrics_humidity
{
    public interface IDeconzApiClient
    {
        public Task<SensorsData> GetLatestSensors(CancellationToken cancel);
    }
}