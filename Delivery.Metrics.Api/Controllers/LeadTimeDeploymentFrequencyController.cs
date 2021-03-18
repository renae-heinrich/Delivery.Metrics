using System.Net;
using System.Threading.Tasks;
using Delivery.Metrics.Common.Contracts;
using Delivery.Metrics.Services;
using Microsoft.AspNetCore.Http;
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
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateReport([FromBody] MetricsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await _reportingService.GenerateReport(request);
            return new OkObjectResult(response);
        }
        
    }
}