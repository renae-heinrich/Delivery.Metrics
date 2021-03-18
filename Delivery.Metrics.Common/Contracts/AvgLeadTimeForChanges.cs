namespace Delivery.Metrics.Common.Contracts
{
    public class AvgLeadTimeForChanges
    {
        public string Name { get; set; }
        public double MergeDelayTime { get; set; }
        public double PipelineDelayTime { get; set; }
        public double TotalDelayTime { get; set; }
    }
}