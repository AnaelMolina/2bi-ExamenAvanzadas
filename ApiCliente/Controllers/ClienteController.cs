using Microsoft.AspNetCore.Mvc;
using ApiCliente.Data;
using ApiCliente.Models;

namespace ApiCliente.Controllers
{
    /// <summary>
    /// Controlador para gestionar clientes
    /// </summary>
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Clientes.ToList());
        }

        /// <summary>
        /// Obtiene un cliente por su ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
                return NotFound(new { message = "Cliente no encontrado" });
            
            return Ok(cliente);
        }

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }

        /// <summary>
        /// Actualiza un cliente existente
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Cliente cliente)
        {
            var existingCliente = _context.Clientes.Find(id);
            if (existingCliente == null)
                return NotFound(new { message = "Cliente no encontrado" });

            existingCliente.Cedula = cliente.Cedula;
            existingCliente.Nombre = cliente.Nombre;
            existingCliente.Telefono = cliente.Telefono;

            _context.SaveChanges();
            return Ok(existingCliente);
        }

        /// <summary>
        /// Elimina un cliente
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
                return NotFound(new { message = "Cliente no encontrado" });

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return Ok(new { message = "Cliente eliminado correctamente" });
        }
    }
}
