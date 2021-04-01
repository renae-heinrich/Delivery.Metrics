using System.Collections.Generic;

namespace Delivery.Metrics.Common.Contracts
{
    public class MetricsRequestDto
    {
        public List<string> Metrics { get; set; } = new List<string>();
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Pipeline Pipeline { get; set; }
        public CodeBaseSetting CodeBaseSetting { get; set; }
    }
}