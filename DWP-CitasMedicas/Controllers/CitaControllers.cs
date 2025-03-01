using DWP_CitasMedicas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
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

    // POST: api/Cita
    [HttpPost]
    public async Task<ActionResult<Cita>> CrearCita([FromBody] Cita cita)
    {
        _context.Cita.Add(cita);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCitas), new { id = cita.IdCita }, cita);
    }

    // PUT: api/Cita/5
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarCita(int id, [FromBody] Cita cita)
    {
        if (id != cita.IdCita)
        {
            return BadRequest();
        }

        _context.Entry(cita).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Cita/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarCita(int id)
    {
        var cita = await _context.Cita.FindAsync(id);
        if (cita == null)
        {
            return NotFound();
        }

        _context.Cita.Remove(cita);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}