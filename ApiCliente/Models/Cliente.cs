using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCliente.Models
{
    /// <summary>
    /// Representa un cliente en el sistema
    /// </summary>
    [Table("cliente")]
    public class Cliente
    {
        /// <summary>
        /// Identificador único del cliente
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Número de cédula del cliente
        /// </summary>
        [Required]
        public string? Cedula { get; set; }

        /// <summary>
        /// Nombre del cliente
        /// </summary>
        [Required]
        public string? Nombre { get; set; }

        /// <summary>
        /// Teléfono del cliente
        /// </summary>
        [Required]
        public string? Telefono { get; set; }
    }
}
