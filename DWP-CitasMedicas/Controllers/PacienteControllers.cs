using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DWP_CitasMedicas.Models;
using System.Threading.Tasks;
using System.Linq;

namespace DWP_CitasMedicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly DwpContext _context;

        public PacienteController(DwpContext context)
        {
            _context = context;
        }

        // Obtener un paciente por cliente
        [HttpGet("byCliente/{idCliente}")]
        public async Task<IActionResult> GetPacienteByCliente(int idCliente)
        {
            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.IdCliente == idCliente);
            if (paciente == null)
            {
                return NotFound("Paciente no encontrado para este cliente.");
            }
            return Ok(paciente);
        }

        // Crear un nuevo paciente
        [HttpPost("byCliente/{idCliente}")]
        public async Task<IActionResult> CreatePaciente(int idCliente, [FromBody] Paciente paciente)
        {
            if (paciente == null)
            {
                return BadRequest("Los datos del paciente son inválidos.");
            }

            var cliente = await _context.Clientes.FindAsync(idCliente);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            paciente.IdCliente = idCliente;
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPacienteByCliente), new { idCliente = paciente.IdCliente }, paciente);
        }

        // Actualizar un paciente existente
        [HttpPut("byCliente/{idCliente}")]
        public async Task<IActionResult> UpdatePaciente(int idCliente, [FromBody] Paciente paciente)
        {
            if (paciente == null || paciente.IdCliente == idCliente)
            {
                return BadRequest("Los datos del paciente son inválidos.");
            }

            var cliente = await _context.Clientes.FindAsync(idCliente);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            _context.Entry(paciente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Eliminar un paciente
        [HttpDelete("byCliente/{idCliente}")]
        public async Task<IActionResult> DeletePaciente(int idCliente)
        {
            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.IdCliente == idCliente);
            if (paciente == null)
            {
                return NotFound("Paciente no encontrado.");
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
