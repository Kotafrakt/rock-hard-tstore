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
        private readonly DatabaseSettings _options;

        public CheckIPMiddleware(RequestDelegate next, IOptions<DatabaseSettings> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var ipAddress = httpContext.Connection.RemoteIpAddress;
            string whiteListIPAddress = _options.AllowedIpAdress;

            if (!ipAddress.Equals(whiteListIPAddress))
            {
                httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                var errorResponse = JsonSerializer.Serialize(
                    new
                    {
                        Code = (int)HttpStatusCode.Forbidden,
                        Message = "Incorrect IP Address, Access denied."
                    });
                await httpContext.Response.WriteAsync(errorResponse);
                return;
            }
            await _next(httpContext);
        }
    }
}
