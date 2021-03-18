using System.Collections.Generic;

namespace Delivery.Metrics.Common.Contracts
{
    public class DeploymentFrequencyOfPipelines
    {
        public string Name { get; set; }
        public string Step { get; set; }
        public string DeploymentFrequency { get; set; }
        public  List<Items> Items { get; set; } = new List<Items>();
    }
}