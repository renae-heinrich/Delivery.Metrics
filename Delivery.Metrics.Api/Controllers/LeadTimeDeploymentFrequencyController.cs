using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
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
        private IMapper _mapper;
        
        public LeadTimeDeploymentFrequencyController(IReportingService reportingService, 
            IMapper mapper)
        {
            _reportingService = reportingService;
            _mapper = mapper;
        }

        [HttpPost("api/generateReport")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateReport([FromBody] MetricsRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            try
            {
                var metricsRequest = _mapper.Map<MetricsRequest>(request);
                var response = await _reportingService.GenerateReport(metricsRequest);
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
}