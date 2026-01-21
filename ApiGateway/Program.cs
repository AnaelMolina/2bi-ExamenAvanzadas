using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiGateway.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Cargar configuración de Ocelot
builder.Configuration.AddJsonFile("ocelot.json", false, true);

// Configurar autenticación JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");
var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured");
var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT Audience not configured");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// Registrar el handler que agrega el header secreto
builder.Services.AddTransient<GatewaySecretHandler>();

// Registrar Ocelot con el handler
builder.Services
    .AddOcelot()
    .AddDelegatingHandler<GatewaySecretHandler>(true);

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

// Middleware de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Mapear los controllers ANTES de Ocelot para que /api/auth y /api/gateway funcionen
app.MapControllers();

// Middleware Ocelot - Solo aplica a rutas que NO son del Gateway local
app.MapWhen(
    context => !context.Request.Path.StartsWithSegments("/api/auth") &&
               !context.Request.Path.StartsWithSegments("/api/gateway"),
    appBuilder => appBuilder.UseOcelot().Wait()
);

app.Run();
