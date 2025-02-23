using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DWP_CitasMedicas.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DWP_CitasMedicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly DwpContext _context;

        public CitaController(DwpContext context)
        {
            _context = context;
        }

        // GET: api/Cita
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitas()
        {
            return await _context.Cita.ToListAsync();
        }

        // GET: api/Cita/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCita(int id)
        {
            var cita = await _context.Cita.FindAsync(id);

            if (cita == null)
            {
                return NotFound("Cita no encontrada.");
            }

            return Ok(cita);
        }

        // POST: api/Cita
        [HttpPost]
        public async Task<ActionResult<Cita>> PostCita([FromBody] Cita cita)
        {
            if (cita == null)
            {
                return BadRequest("Datos de cita inválidos.");
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCita), new { id = cita.IdCita }, cita);
        }

        // PUT: api/Cita/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, [FromBody] Cita cita)
        {
            if (id != cita.IdCita)
            {
                return BadRequest("ID de cita no coincide.");
            }

            _context.Entry(cita).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Cita/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await _context.Cita.FindAsync(id);

            if (cita == null)
            {
                return NotFound("Cita no encontrada.");
            }

            _context.Cita.Remove(cita);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
