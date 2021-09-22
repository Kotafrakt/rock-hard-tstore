using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using TransactionStore.Core;

namespace TransactionStore.API.Configuration
{
    public class CheckIPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _options;

        public CheckIPMiddleware(RequestDelegate next, IOptions<AppSettings> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var ipAddress = httpContext.Connection.RemoteIpAddress;
            var whiteListIPAddress = _options.AllowedIpAddress;

            if (ipAddress.ToString() != whiteListIPAddress)
            {
                httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                var errorResponse = JsonSerializer.Serialize(
                    new
                    {
                        Code = (int)HttpStatusCode.Forbidden,
                        Message = $"{ipAddress} is Incorrect IP Address, Access denied."
                    });
                await httpContext.Response.WriteAsync(errorResponse);
                return;
            }
            await _next(httpContext);
        }
    }
}
