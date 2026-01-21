using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    /// <summary>
    /// Controller para health check del Gateway
    /// </summary>
    [ApiController]
    [Route("api/gateway")]
    public class GatewayController : ControllerBase
    {
        private readonly ILogger<GatewayController> _logger;

        public GatewayController(ILogger<GatewayController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Endpoint de health check - Los microservicios lo consultan para saber si el Gateway está activo
        /// </summary>
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new 
            { 
                status = "active",
                message = "API Gateway está activo",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
