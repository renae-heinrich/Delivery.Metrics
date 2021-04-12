using System.Collections.Generic;

namespace Delivery.Metrics.Common.Contracts
{
    public class LeadTimeForChanges
    {
        public List<LeadTimeForChangesOfPipelines> LeadTimeForChangesOfPipelines { get; set; }
        public List<AvgLeadTimeForChanges> AvgLeadTimeForChanges { get; set; }
    }
}