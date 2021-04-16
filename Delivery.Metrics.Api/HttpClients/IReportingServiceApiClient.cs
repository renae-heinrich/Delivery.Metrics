using System.Threading.Tasks;
using Delivery.Metrics.Common.Contracts;

namespace Delivery.Metrics.HttpClients
{
    public interface IReportingServiceApiClient
    { 
        Task<ReportResponse> GetReport(MetricsRequest request);
    }
}