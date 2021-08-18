using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Delivery.Metrics.Common.Contracts;

namespace Delivery.Metrics.HttpClients
{
    public class ReportingServiceApiClient : IReportingServiceApiClient
    {
        private readonly HttpClient _httpClient;

        public ReportingServiceApiClient(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ReportResponse> GetReport(MetricsRequest request)
        {
            var endpointUrl = "https://heartbeat-backend.svc.platform.myobdev.com/generateReporter";

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpointUrl))
            {
                request.Pipeline.Token = "a29412d98b1465289c6234b9936e927ce5c43dd6";
                request.CodeBaseSetting.Token = "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";
                var response = await SendRequest(JsonSerializer.Serialize(request), requestMessage);
                return JsonSerializer.Deserialize<ReportResponse>(response);
            }
        }

        private async Task<string> SendRequest(string body,  HttpRequestMessage requestMessage)
        {
            if (body != null)
            {
                requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }
            
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.SendAsync(requestMessage);
            }
            catch (Exception e)
            {
                throw new CustomHttpRequestException($"{e.Message} {e.InnerException}", "555");
            }

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Unauthorized");
                }
                throw new Exception(response.StatusCode.ToString());
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}