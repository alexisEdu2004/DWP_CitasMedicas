﻿using DWP_CitasMedicas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PacienteController : ControllerBase
{
    private readonly DwpContext _context;

    public PacienteController(DwpContext context)
    {
        _context = context;
    }

    // GET: api/Paciente
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
    {
        return await _context.Pacientes.ToListAsync();
    }

    // GET: api/Paciente/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Paciente>> GetPaciente(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null)
        {
            return NotFound();
        }
        return paciente;
    }

    // POST: api/Paciente
    [HttpPost]
    public async Task<ActionResult<Paciente>> CrearPaciente([FromBody] Paciente paciente)
    {
        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPaciente), new { id = paciente.IdPaciente }, paciente);
    }

    // PUT: api/Paciente/5
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarPaciente(int id, [FromBody] Paciente paciente)
    {
        if (id != paciente.IdPaciente)
        {
            return BadRequest();
        }

        _context.Entry(paciente).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Paciente/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarPaciente(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null)
        {
            return NotFound();
        }

        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}