using ApiEmpleado.Services;

namespace ApiEmpleado.Middleware
{
    /// <summary>
    /// Middleware que valida que las peticiones vengan del API Gateway
    /// Solo bloquea si el Gateway está activo
    /// </summary>
    public class GatewayAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GatewayAuthMiddleware> _logger;

        public GatewayAuthMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<GatewayAuthMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Permitir Swagger y endpoints de estado sin validación
            if (context.Request.Path.StartsWithSegments("/swagger") ||
                context.Request.Path.StartsWithSegments("/api/gateway-status"))
            {
                await _next(context);
                return;
            }

            // Si el Gateway NO está activo, permitir todas las peticiones
            if (!GatewayStatusService.IsGatewayActive)
            {
                _logger.LogDebug("Gateway no activo - Permitiendo acceso directo");
                await _next(context);
                return;
            }

            // Gateway ACTIVO - Verificar el header secreto
            var expectedKey = _configuration["GatewaySecret"] ?? "MiClaveSecretaDelGateway123";

            if (!context.Request.Headers.TryGetValue("X-Gateway-Secret", out var gatewaySecret) ||
                gatewaySecret != expectedKey)
            {
                _logger.LogWarning("⛔ Acceso BLOQUEADO - Intento de acceso directo desde: {IP}", 
                    context.Connection.RemoteIpAddress);
                    
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new 
                { 
                    message = "Acceso denegado. El API Gateway está activo. Use el Gateway para acceder.",
                    error = "Forbidden",
                    gatewayUrl = "http://localhost:5003"
                });
                return;
            }

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
