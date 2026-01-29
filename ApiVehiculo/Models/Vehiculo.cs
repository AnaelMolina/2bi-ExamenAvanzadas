using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVehiculo.Models
{
    /// <summary>
    /// Representa un vehículo en el sistema
    /// </summary>
    [Table("vehiculo")]
    public class Vehiculo
    {
        /// <summary>
        /// Identificador único del vehículo
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Placa del vehículo
        /// </summary>
        [Required]
        public string? Placa { get; set; }

        /// <summary>
        /// Provincia donde está registrado el vehículo
        /// </summary>
        [Required]
        public string? Provincia { get; set; }
    }
}
