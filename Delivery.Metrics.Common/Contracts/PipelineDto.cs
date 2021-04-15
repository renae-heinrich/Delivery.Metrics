using System.Collections.Generic;

namespace Delivery.Metrics.Common.Contracts
{
    public class PipelineDto
    {
        public string Token { get; set; }
        public List<Deployment> Deployment { get; set; } = new List<Deployment>();
    }
}