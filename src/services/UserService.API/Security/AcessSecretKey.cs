using Microsoft.Extensions.Options;

namespace UserService.API.Security
{
    public class AcessSecretKey(IOptions<SecurityKey> options, RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private readonly SecurityKey _config = options.Value;

        public async Task InvokeAsync(HttpContext context)
        {

            if (!context.Request.Headers.TryGetValue("AcessSecretKey", out var providedSecretKey) ||
                providedSecretKey != _config.Key)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied: Invalid or missing AcessSecretKey");
                return;
            }

            await _next(context);
        }
    }
}
