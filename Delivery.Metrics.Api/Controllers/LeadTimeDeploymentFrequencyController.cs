using System.Threading.Tasks;
using Delivery.Metrics.Common.Contracts;
using Delivery.Metrics.Services;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Metrics.Controllers
{
    [Produces("application/json")]
    public class LeadTimeDeploymentFrequencyController : ControllerBase
    {
        private readonly IReportingService _reportingService;
        
        public LeadTimeDeploymentFrequencyController(IReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        [HttpPost("api/generateReport")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GenerateReport([FromBody] MetricsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _reportingService.GenerateReport(request);
            return Ok();
        }
        
    }
}