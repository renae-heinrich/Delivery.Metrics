using System.Collections.Generic;

namespace Delivery.Metrics.Common.Contracts
{
    public class DeploymentFrequency
    {
        public AvgDeploymentFrequency AvgDeploymentFrequency { get; set; }
        public List<DeploymentFrequencyOfPipelines> DeploymentFrequencyOfPipelines { get; set; } = new List<DeploymentFrequencyOfPipelines>();
    }
}