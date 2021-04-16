using System.Text.Json.Serialization;

namespace Delivery.Metrics.Common.Contracts
{
    public class Deployment
    {
        public string OrgId { get; set; } = "myob";
        public string OrgName { get; set; } = "MYOB";
        public string Id { get; set; }
        public string Name { get; set; }
        public string Step { get; set; }
        public string Repository { get; set; }
    }
}