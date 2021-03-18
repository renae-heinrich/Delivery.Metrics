using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.Metrics.Common.Contracts;
using Delivery.Metrics.Controllers;
using Delivery.Metrics.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Delivery.Metrics.Api.Tests.Controllers
{
    public class LeadTimeDeploymentFrequencyControllerTests
    {
        private readonly IReportingService _reportingService;
        private readonly LeadTimeDeploymentFrequencyController _controller;

        public LeadTimeDeploymentFrequencyControllerTests()
        {
            _reportingService = Substitute.For<IReportingService>();
            _controller = new LeadTimeDeploymentFrequencyController(_reportingService);
            
        }

        [Fact]
        public async Task GenerateReport_ShouldReturnOkObjectResult_WhenValidRequest()
        {
            var request = new MetricsRequest
            {
                Metrics = new List<string> {"some string"},
                StartTime = 1612098000000,
                EndTime = 1612098000000,
                Pipeline = new Pipeline
                {
                    Type = "someType",
                    Token = "someToken",
                    Deployment = new List<Deployment>
                    {
                        new Deployment
                        {
                            OrgId = "someOrgId",
                            OrgName = "someOrgName",
                            Id = "someId",
                            Name = "someName",
                            Step = "someStep",
                            Repository = "someRepository",
                        }
                    }
                },
                CodeBaseSetting = new CodeBaseSetting
                {
                    Type = "someType",
                    Token = "someToken",
                    LeadTime = new List<LeadTime>
                    {
                        new LeadTime
                        {
                            OrgId = "someOrgId",
                            OrgName = "someOrgName",
                            Id = "someId",
                            Name = "someName",
                            Step = "someStep",
                            Repository = "someRepository",
                        }
                    }
                },
                CsvTimeStamp = 1614140301623
            };

            var actual = await _controller.GenerateReport(request);

            var result = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(200, result.StatusCode);
        }
    }
}