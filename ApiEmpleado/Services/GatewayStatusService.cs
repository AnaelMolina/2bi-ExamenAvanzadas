namespace ApiEmpleado.Services
{
    /// <summary>
    /// Servicio que monitorea si el API Gateway está activo
    /// </summary>
    public class GatewayStatusService : IHostedService, IDisposable
    {
        private readonly ILogger<GatewayStatusService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private Timer? _timer;
        
        // Estado del Gateway - accesible desde el middleware
        public static bool IsGatewayActive { get; private set; } = false;
        
        public GatewayStatusService(ILogger<GatewayStatusService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(3);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando servicio de monitoreo del Gateway...");
            
            // Verificar cada 5 segundos si el Gateway está activo
            _timer = new Timer(CheckGatewayStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            
            return Task.CompletedTask;
        }

        private async void CheckGatewayStatus(object? state)
        {
            var gatewayUrl = _configuration["GatewayHealthUrl"] ?? "http://localhost:5003/api/gateway/health";
            
            try
            {
                var response = await _httpClient.GetAsync(gatewayUrl);
                
                if (response.IsSuccessStatusCode)
                {
                    if (!IsGatewayActive)
                    {
                        _logger.LogWarning("🟢 API Gateway DETECTADO - Activando protección de rutas");
                    }
                    IsGatewayActive = true;
                }
                else
                {
                    SetGatewayInactive();
                }
            }
            catch (Exception)
            {
                SetGatewayInactive();
            }
        }

        private void SetGatewayInactive()
        {
            if (IsGatewayActive)
            {
                _logger.LogWarning("🔴 API Gateway NO DISPONIBLE - Desactivando protección de rutas");
            }
            IsGatewayActive = false;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deteniendo servicio de monitoreo del Gateway...");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _httpClient.Dispose();
        }
    }
}
