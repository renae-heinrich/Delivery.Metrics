using System;

namespace Delivery.Metrics.HttpClients
{
    internal class CustomHttpRequestException : Exception
    {
        public string? StatusCode { get; }
        public CustomHttpRequestException()
        {
            StatusCode = null;
        }
        public CustomHttpRequestException(string message, string statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}