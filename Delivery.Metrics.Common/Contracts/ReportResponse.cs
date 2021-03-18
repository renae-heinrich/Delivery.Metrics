namespace Delivery.Metrics.Common.Contracts
{
    public class ReportResponse
    {
        public DeploymentFrequency DeploymentFrequency { get; set; }
        public LeadTimeForChanges LeadTimeForChanges { get; set; }
        
    }
}