using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.Metrics.Common.Contracts;
using Delivery.Metrics.HttpClients;
using Delivery.Metrics.Services;
using NSubstitute;
using Xunit;

namespace Delivery.Metrics.Api.Tests.Services
{
    public class ReportingServiceTests
    {
        private readonly ReportingService _reportingService;
        private readonly IReportingServiceApiClient _reportingServiceApiClient;

        public ReportingServiceTests()
        {
            _reportingService = new ReportingService();
            _reportingServiceApiClient = Substitute.For<IReportingServiceApiClient>();
        }

        [Fact]
        public async Task GenerateReport_CallsClient_GivenAValidRequest()
        {
            var id = "1";
            var name = "someName";
            var orgId = "someId";
            var repo = "repo";
            var orgName = "someOrg";
            var step = "someStep";
            
                var request = new MetricsRequest
            {
                Metrics = new List<string>{"some metric"},
                ConsiderHoliday = false,
                CodeBaseSetting = new CodeBaseSetting
                {
                    LeadTime = new List<LeadTime>
                    {
                        new LeadTime
                        {
                            Id = id,
                            Name = name,
                            OrgId = orgId,
                            Repository = repo,
                            OrgName = orgName,
                            Step = step
                        }
                    }
                },
                Pipeline = new Pipeline
                {
                    Deployment = new List<Deployment>
                    {
                        new Deployment
                        {
                            Id = id,
                            Name = name,
                            OrgId = orgId,
                            Repository = repo,
                            OrgName = orgName,
                            Step = step
                        }
                    },
                    Token = "someToken",
                    Type = "someType"
                }
                
            };
            await _reportingService.GenerateReport(request);

            await _reportingServiceApiClient.Received(1).GetReport(request);
        }
    }
}