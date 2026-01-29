namespace ApiVehiculo.Services
{
    /// <summary>
    /// Servicio para monitorear el estado del Gateway
    /// </summary>
    public class GatewayStatusService : BackgroundService
    {
        private readonly ILogger<GatewayStatusService> _logger;

        public GatewayStatusService(ILogger<GatewayStatusService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("GatewayStatusService iniciado");

            while (!stoppingToken.IsCancellationRequested)
            {
                // Lógica de monitoreo del Gateway aquí
                await Task.Delay(5000, stoppingToken);
            }

            _logger.LogInformation("GatewayStatusService finalizado");
        }
    }
}
