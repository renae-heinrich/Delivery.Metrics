using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Metrics.Common.Contracts;
using Delivery.Metrics.Controllers;
using Delivery.Metrics.Profiles;
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
        private readonly IMapper _mapper;

        public LeadTimeDeploymentFrequencyControllerTests()
        {
            _reportingService = Substitute.For<IReportingService>();
            
            var myProfile = new MetricsRequestProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            _controller = new LeadTimeDeploymentFrequencyController(_reportingService, _mapper);
        }

        [Fact]
        public async Task GenerateReport_ShouldReturnOkObjectResult_WhenValidRequest()
        {
            //Arrange
            var request = new MetricsRequestDto
            {
                StartDate = "01/02/2021",
                EndDate = "01/02/2021",
                Metrics = new List<string>{"some metric"},
                PipelineDto = new PipelineDto
                {
                    Token = "someToken",
                    Deployment= new List<Deployment>
                    {
                        new Deployment
                        {
                            Id = "someId",
                            Name = "someName",
                            Step = "someStep",
                            Repository = "someRepository",
                        }
                    }
                },
                 CodeBaseSettingDto = new CodeBaseSettingDto
                 {
                     Token = "someToken",
                     LeadTime = new List<LeadTime>
                     {
                         new LeadTime
                         {
                             Id = "someId",
                             Name = "someName",
                             Step = "someStep",
                             Repository = "someRepository",
                         }
                     }
                }
            };
            
            //Act
            var actual = await _controller.GenerateReport(request);
            
            //Assert
            var result = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(200, result.StatusCode);
            await _reportingService.Received(1).GenerateReport(Arg.Any<MetricsRequest>());
        }
        
        [Fact]
        public async Task GenerateReport_ShouldReturnUnAuthorised_WhenServiceThrowsUnauthorizedAccessException()
        {
            //Arrange
            _reportingService.When(x => x.GenerateReport(Arg.Any<MetricsRequest>()))
                .Do(x => throw new UnauthorizedAccessException());

            //Act
            var actual = await _controller.GenerateReport(new MetricsRequestDto());
            
            //Assert
            var result = Assert.IsType<UnauthorizedObjectResult>(actual);
            Assert.Equal(401, result.StatusCode);
        }
        
         [Fact]
        public async Task GenerateReport_ShouldReturnObjectResult_WhenServiceThrowsException()
        {
            //Arrange
            _reportingService.When(x => x.GenerateReport(Arg.Any<MetricsRequest>()))
                .Do(x => throw new Exception());

            //Act
            var actual = await _controller.GenerateReport(new MetricsRequestDto());
            
            //Assert
            var result = Assert.IsType<ObjectResult>(actual);
            Assert.Equal(500, result.StatusCode);
        }
        
        [Theory]
        [MemberData(nameof(RequestInvalidValues))]
        public async Task GenerateReport_ShouldReturnBadRequest_WhenInvalidRequest(MetricsRequest request, string propertyName)
        {
            //Arrange
            _controller.ModelState.AddModelError(propertyName, "cannot be null");
            
            //Act
            var actual = await _controller.GenerateReport(new MetricsRequestDto());

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