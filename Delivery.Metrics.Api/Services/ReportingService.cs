using System.Threading.Tasks;
using Delivery.Metrics.Common.Contracts;

namespace Delivery.Metrics.Services
{
    public class ReportingService : IReportingService
    {
        public Task<ReportResponse> GenerateReport(MetricsRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}