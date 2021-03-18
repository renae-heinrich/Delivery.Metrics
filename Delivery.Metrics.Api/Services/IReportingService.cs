using System.Threading.Tasks;
using Delivery.Metrics.Common.Contracts;

namespace Delivery.Metrics.Services
{
    public interface IReportingService
    {
        public Task GenerateReport(MetricsRequest request);
    }
}