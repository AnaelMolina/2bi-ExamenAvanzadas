using Microsoft.EntityFrameworkCore;
using ApiEmpleado.Data;
using ApiEmpleado.Middleware;
using ApiEmpleado.Services;

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware para validar que las peticiones vengan del Gateway
app.UseGatewayAuth();

app.UseAuthorization();
app.MapControllers();

app.Run();
