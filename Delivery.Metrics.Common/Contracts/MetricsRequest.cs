using System;
using System.Collections.Generic;

namespace Delivery.Metrics.Common.Contracts
{
    public class MetricsRequest
    {   
        public List<string> Metrics { get; set; } = new List<string>();
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public bool ConsiderHoliday { get; set; }
        public Pipeline Pipeline { get; set; }
        public CodeBaseSetting CodeBaseSetting { get; set; }
        public long CsvTimeStamp { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();
    }
}