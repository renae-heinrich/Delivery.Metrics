using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Delivery.Metrics.Common.Contracts;
using Delivery.Metrics.HttpClients;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;
using NSubstitute;
using RichardSzalay.MockHttp;
using Xunit;

namespace Delivery.Metrics.Api.Tests.HttpClients
{
    public class ReportingServiceApiClientTests
    {
        private ReportingServiceApiClient _client;
        private readonly string _responseContent;

        public ReportingServiceApiClientTests()
        {
            _responseContent = JsonConvert.SerializeObject(new ReportResponse());
        }
        
        [Fact]
        public async Task GetReport_ReturnsReportResponse_WhenClientReturnsSuccess()
        {
            //Arrange
            var mockHttpClient = CreateClient(_responseContent);
            
            _client = new ReportingServiceApiClient(mockHttpClient);

            var actual = await _client.GetReport(new MetricsRequest
            {
                Pipeline = new Pipeline(),
                CodeBaseSetting = new CodeBaseSetting()
            });
            
            Assert.IsType<ReportResponse>(actual);
        }
        
        [Fact]
        public async Task GetReport_ThrowsUnauthorizedAccessException_WhenClientReturnsUnauthorised()
        {
            //Arrange
            var mockHttpClient = CreateClient(_responseContent, HttpStatusCode.Unauthorized);
            
            _client = new ReportingServiceApiClient(mockHttpClient);
            
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await _client.GetReport(Arg.Any<MetricsRequest>()));
        }
        
        [Fact]
        public async Task GetReport_ThrowsBadRequestException_WhenClientReturnsBadRequest()
        {
            //Arrange
            var mockHttpClient = CreateClient(_responseContent, HttpStatusCode.BadRequest);
            
            _client = new ReportingServiceApiClient(mockHttpClient);
            
            await Assert.ThrowsAsync<Exception>(async () =>
                await _client.GetReport(Arg.Any<MetricsRequest>()));
        }
        
        private static HttpClient CreateClient(string responseContent, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When($"/generateReporter")
                .Respond(statusCode, "application/json", responseContent);

            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("https://heartbeat-backend.svc.platform.myobdev.com");

            return httpClient;
        }
        
    }
}