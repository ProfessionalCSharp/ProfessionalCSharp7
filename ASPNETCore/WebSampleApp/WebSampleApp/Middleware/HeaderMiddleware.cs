using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebSampleApp.Middleware
{
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderMiddleware(RequestDelegate next) =>
            _next = next;

        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("SampleHeader", new[] { "addheadermiddleware" });
            return _next(httpContext);
        }
    }

    public static class HeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<HeaderMiddleware>();
    }
}
