using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEmpleado.Models
{
    /// <summary>
    /// Representa un empleado en el sistema
    /// </summary>
    [Table("empleado")]
    public class Empleado
    {
        /// <summary>
        /// Identificador único del empleado
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Número de cédula del empleado
        /// </summary>
        [Required]
        public string? Cedula { get; set; }

        /// <summary>
        /// Nombres del empleado
        /// </summary>
        [Required]
        public string? Nombres { get; set; }

        /// <summary>
        /// Apellidos del empleado
        /// </summary>
        [Required]
        public string? Apellidos { get; set; }
    }
}
