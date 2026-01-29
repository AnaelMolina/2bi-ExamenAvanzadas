using Microsoft.EntityFrameworkCore;
using ApiCliente.Models;

namespace ApiCliente.Data
{
    /// <summary>
    /// Contexto de base de datos para clientes
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// DbSet de clientes
        /// </summary>
        public DbSet<Cliente> Clientes { get; set; }
    }
}
