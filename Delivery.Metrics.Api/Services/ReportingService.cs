
using System.Text.Json;
using System.Threading.Tasks;
using Delivery.Metrics.Common.Contracts;
using Delivery.Metrics.HttpClients;


namespace Delivery.Metrics.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IReportingServiceApiClient _reportingServiceApiClient;

        public ReportingService(IReportingServiceApiClient reportingServiceApiClient)
        {
            _reportingServiceApiClient = reportingServiceApiClient;
        }

        public async Task<string> GenerateReport(MetricsRequest request)
        {
            return JsonSerializer.Serialize(await _reportingServiceApiClient.GetReport(request));
        }
    }
}