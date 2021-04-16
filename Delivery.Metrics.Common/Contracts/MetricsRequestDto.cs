using System.Collections.Generic;

namespace Delivery.Metrics.Common.Contracts
{
    public class MetricsRequestDto
    {
        public List<string> Metrics { get; set; } = new List<string>();
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public PipelineDto PipelineDto { get; set; }
        public CodeBaseSettingDto CodeBaseSettingDto { get; set; }
    }
}