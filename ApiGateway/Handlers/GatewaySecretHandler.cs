namespace ApiGateway.Handlers
{
    /// <summary>
    /// Handler que agrega el header secreto a todas las peticiones que van hacia los microservicios
    /// </summary>
    public class GatewaySecretHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken)
        {
            // Agregar el header secreto a la petición
            request.Headers.Add("X-Gateway-Secret", "MiClaveSecretaDelGateway123");
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
