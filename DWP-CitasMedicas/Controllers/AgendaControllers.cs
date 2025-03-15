using DWP_CitasMedicas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AgendaController : ControllerBase
{
    private readonly DwpContext _context;

    public AgendaController(DwpContext context)
    {
        _context = context;
    }

    // GET: api/Agenda/Disponibles
    [HttpGet("Disponibles")]
    public async Task<ActionResult<IEnumerable<Agenda>>> GetAgendasDisponibles()
    {
        return await _context.Agenda
            .Where(a => a.FechaCita > DateTime.Now)
            .ToListAsync();
    }

    // POST: api/Agenda
    [HttpPost]
    public async Task<ActionResult<Agenda>> CrearAgenda([FromBody] Agenda agenda)
    {
        // Verificar si el modelo es válido
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Verificar si el doctor existe
        if (!_context.Doctors.Any(d => d.IdDoctor == agenda.IdDoctor))
        {
            return BadRequest("El doctor especificado no existe.");
        }

        // Guardar la agenda
        _context.Agenda.Add(agenda);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAgendasDisponibles), new { id = agenda.IdAgenda }, agenda);
    }
}