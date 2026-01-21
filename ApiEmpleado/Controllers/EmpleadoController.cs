using Microsoft.AspNetCore.Mvc;
using ApiEmpleado.Data;
using ApiEmpleado.Models;

namespace ApiEmpleado.Controllers
{
    /// <summary>
    /// Controlador para gestionar empleados
    /// </summary>
    [ApiController]
    [Route("api/empleados")]
    public class EmpleadoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpleadoController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los empleados
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Empleados.ToList());
        }

        /// <summary>
        /// Obtiene un empleado por su ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado == null)
                return NotFound(new { message = "Empleado no encontrado" });
            
            return Ok(empleado);
        }

        /// <summary>
        /// Crea un nuevo empleado
        /// </summary>
        [HttpPost]
        public IActionResult Create(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = empleado.Id }, empleado);
        }

        /// <summary>
        /// Actualiza un empleado existente
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Empleado empleado)
        {
            var existingEmpleado = _context.Empleados.Find(id);
            if (existingEmpleado == null)
                return NotFound(new { message = "Empleado no encontrado" });

            existingEmpleado.Cedula = empleado.Cedula;
            existingEmpleado.Nombres = empleado.Nombres;
            existingEmpleado.Apellidos = empleado.Apellidos;

            _context.SaveChanges();
            return Ok(existingEmpleado);
        }

        /// <summary>
        /// Elimina un empleado
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado == null)
                return NotFound(new { message = "Empleado no encontrado" });

            _context.Empleados.Remove(empleado);
            _context.SaveChanges();
            return Ok(new { message = "Empleado eliminado correctamente" });
        }
    }
}
