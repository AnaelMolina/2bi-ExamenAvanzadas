using Microsoft.AspNetCore.Mvc;
using ApiEmpleado.Services;

namespace ApiEmpleado.Controllers
{
    /// <summary>
    /// Controller para recibir notificaciones del Gateway
    /// </summary>
    [ApiController]
    [Route("api/gateway-status")]
    public class GatewayStatusController : ControllerBase
    {
        private readonly ILogger<GatewayStatusController> _logger;
        private readonly IConfiguration _configuration;

        public GatewayStatusController(ILogger<GatewayStatusController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Endpoint para que el Gateway notifique que está activo
        /// </summary>
        [HttpPost("notify")]
        public IActionResult NotifyGatewayActive([FromHeader(Name = "X-Gateway-Secret")] string? secret)
        {
            var expectedSecret = _configuration["GatewaySecret"] ?? "MiClaveSecretaDelGateway123";
            
            if (secret != expectedSecret)
            {
                return Unauthorized(new { message = "Secret inválido" });
            }

            _logger.LogWarning("🟢 Gateway notificó que está ACTIVO - Protección activada");
            return Ok(new { message = "Notificación recibida", protectionActive = true });
        }

        /// <summary>
        /// Obtener el estado actual de la protección
        /// </summary>
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new 
            { 
                gatewayActive = GatewayStatusService.IsGatewayActive,
                protectionEnabled = GatewayStatusService.IsGatewayActive,
                message = GatewayStatusService.IsGatewayActive 
                    ? "Protección ACTIVA - Solo se permiten peticiones via Gateway" 
                    : "Protección INACTIVA - Gateway no detectado"
            });
        }
    }
}
