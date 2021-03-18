using System.Collections.Generic;

namespace Delivery.Metrics.Common.Contracts
{
    public class CodeBaseSetting
    {
        public string Type { get; set; }
        public string Token { get; set; }
        public List<LeadTime> LeadTime { get; set; } = new List<LeadTime>();
    }
}