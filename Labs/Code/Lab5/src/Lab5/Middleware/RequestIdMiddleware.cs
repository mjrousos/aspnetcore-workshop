using Lab5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Lab5.Middleware
{
    public class RequestIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestIdMiddleware> _logger;
        private readonly IRequestIdFactory _requestIdFactory;

        public RequestIdMiddleware(RequestDelegate next, IRequestIdFactory requestIdFactory, ILogger<RequestIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _requestIdFactory = requestIdFactory;
        }

        public Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"Request {_requestIdFactory.MakeRequestId()} executing.");

            return _next(context);
        }
    }
}