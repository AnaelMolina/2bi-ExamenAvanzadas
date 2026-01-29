using Microsoft.EntityFrameworkCore;
using ApiVehiculo.Models;

namespace ApiVehiculo.Data
{
    /// <summary>
    /// Contexto de base de datos para vehículos
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// DbSet de vehículos
        /// </summary>
        public DbSet<Vehiculo> Vehiculos { get; set; }
    }
}
