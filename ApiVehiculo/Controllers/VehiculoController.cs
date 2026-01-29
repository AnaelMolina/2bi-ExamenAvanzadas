using Microsoft.AspNetCore.Mvc;
using ApiVehiculo.Data;
using ApiVehiculo.Models;

namespace ApiVehiculo.Controllers
{
    /// <summary>
    /// Controlador para gestionar vehículos
    /// </summary>
    [ApiController]
    [Route("api/vehiculos")]
    public class VehiculoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VehiculoController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los vehículos
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Vehiculos.ToList());
        }

        /// <summary>
        /// Obtiene un vehículo por su ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var vehiculo = _context.Vehiculos.Find(id);
            if (vehiculo == null)
                return NotFound(new { message = "Vehículo no encontrado" });
            
            return Ok(vehiculo);
        }

        /// <summary>
        /// Crea un nuevo vehículo
        /// </summary>
        [HttpPost]
        public IActionResult Create(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = vehiculo.Id }, vehiculo);
        }

        /// <summary>
        /// Actualiza un vehículo existente
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Vehiculo vehiculo)
        {
            var existingVehiculo = _context.Vehiculos.Find(id);
            if (existingVehiculo == null)
                return NotFound(new { message = "Vehículo no encontrado" });

            existingVehiculo.Placa = vehiculo.Placa;
            existingVehiculo.Provincia = vehiculo.Provincia;

            _context.SaveChanges();
            return Ok(existingVehiculo);
        }

        /// <summary>
        /// Elimina un vehículo
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vehiculo = _context.Vehiculos.Find(id);
            if (vehiculo == null)
                return NotFound(new { message = "Vehículo no encontrado" });

            _context.Vehiculos.Remove(vehiculo);
            _context.SaveChanges();
            return Ok(new { message = "Vehículo eliminado correctamente" });
        }
    }
}
