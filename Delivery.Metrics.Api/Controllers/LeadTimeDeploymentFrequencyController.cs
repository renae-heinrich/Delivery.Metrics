using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> GenerateReport([FromBody] MetricsRequestDto request)
        {
            
            //TODO: Need to map a DTO to MetricsRequest so user doesnt have to put in the unix code of the time
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            try
            {
                var response = await _reportingService.GenerateReport(new MetricsRequest());
                return new OkObjectResult(response);
            }
            catch (UnauthorizedAccessException e)
            {
                return new UnauthorizedObjectResult(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
    }

    public class MetricsRequestDto
    {
        public List<string> Metrics { get; set; } = new List<string>();
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public PipelineDto Pipeline { get; set; }
        public CodeBaseSettingDto CodeBaseSetting { get; set; }
    }

    public class PipelineDto
    {
        public string Type { get; set; }
        public string Token { get; set; }
        public List<DeploymentDto> Deployment { get; set; } = new List<DeploymentDto>();

    }

    public class CodeBaseSettingDto
    {
        public string Type { get; set; }
        public string Token { get; set; }
        public List<LeadTimeDto> LeadTime { get; set; } = new List<LeadTimeDto>();

    }

    public class LeadTimeDto
    {
        
        public string OrgId { get; set; }
        public string OrgName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Step { get; set; }
        public string Repository { get; set; }
        
    }

    public class DeploymentDto
    {
        public string OrgId { get; set; }
        public string OrgName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Step { get; set; }
        public string Repository { get; set; }

    }
}