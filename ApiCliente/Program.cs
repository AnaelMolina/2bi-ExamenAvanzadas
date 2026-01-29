using Microsoft.EntityFrameworkCore;
using ApiCliente.Data;
using ApiCliente.Middleware;
using ApiCliente.Services;
using Consul;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext con PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar el servicio de monitoreo del Gateway
builder.Services.AddHostedService<GatewayStatusService>();

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar Consul Client
builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(config =>
{
    config.Address = new Uri("http://consul:8500");
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new { status = "healthy", service = "api-cliente" }))
    .WithName("Health");

// Middleware para validar que las peticiones vengan del Gateway
app.UseGatewayAuth();

app.UseAuthorization();
app.MapControllers();

// Registrar el servicio en Consul
var consulClient = app.Services.GetRequiredService<IConsulClient>();
var serviceName = "api-cliente";
var serviceId = $"{serviceName}-1";
var registration = new AgentServiceRegistration()
{
    ID = serviceId,
    Name = serviceName,
    Address = "api-cliente",
    Port = 5005,
    Check = new AgentServiceCheck()
    {
        HTTP = "http://api-cliente:5005/health",
        Interval = TimeSpan.FromSeconds(10),
        Timeout = TimeSpan.FromSeconds(5),
        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(30)
    }
};

await consulClient.Agent.ServiceRegister(registration);

app.Run("http://0.0.0.0:5005");
