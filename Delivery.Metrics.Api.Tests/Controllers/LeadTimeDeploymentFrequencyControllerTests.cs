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
            //Arrange
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

            //Act
            var actual = await _controller.GenerateReport(request);
            
            //Assert
            var result = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(200, result.StatusCode);
            await _reportingService.Received(1).GenerateReport(request);
        }
        
        [Theory]
        [MemberData(nameof(RequestInvalidValues))]
        public async Task GenerateReport_ShouldReturnBadRequest_WhenInvalidRequest(MetricsRequest request, string propertyName)
        {
            //Arrange
            _controller.ModelState.AddModelError(propertyName, "cannot be null");
            
            //Act
            var actual = await _controller.GenerateReport(request);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actual);
            Assert.Equal(400, result.StatusCode);
            await _reportingService.DidNotReceive().GenerateReport(Arg.Any<MetricsRequest>());
        }

        public static IEnumerable<object[]> RequestInvalidValues()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new MetricsRequest {}, nameof(MetricsRequest)
                }
            };
        }
    }
}