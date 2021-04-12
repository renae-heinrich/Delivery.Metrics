namespace Delivery.Metrics.Common.Contracts
{
    public class LeadTimeForChangesOfPipelines
    {
        public string Name { get; set; }
        public string Step { get; set; }
        public double MergeDelayTime { get; set; }
        public double PipelineDelayTime { get; set; }
        public double TotalDelayTime { get; set; }
    }
}