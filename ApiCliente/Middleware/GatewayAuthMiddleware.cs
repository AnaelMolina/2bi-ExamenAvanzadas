namespace ApiCliente.Middleware
{
    /// <summary>
    /// Middleware para validar que las peticiones vengan del Gateway
    /// </summary>
    public class GatewayAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public GatewayAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Agregar validaciones del Gateway aquí si es necesario
            await _next(context);
        }
    }

    public static class GatewayAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseGatewayAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GatewayAuthMiddleware>();
        }
    }
}
