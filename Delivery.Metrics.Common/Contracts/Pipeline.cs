using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Delivery.Metrics.Common.Contracts
{
    public class Pipeline
    {
        public string Type { get; set; }
        public string Token { get; set; }
        public List<Deployment> Deployment { get; set; } = new List<Deployment>();
    }
}