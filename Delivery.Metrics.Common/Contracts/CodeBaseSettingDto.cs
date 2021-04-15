using System.Collections.Generic;

namespace Delivery.Metrics.Common.Contracts
{
    public class CodeBaseSettingDto {
        public string Token { get; set; }
        public List<LeadTime> LeadTime { get; set; } = new List<LeadTime>();
    }
}