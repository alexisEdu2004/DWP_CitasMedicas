using DWP_CitasMedicas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly DwpContext _context;

    public DoctorController(DwpContext context)
    {
        _context = context;
    }

    // GET: api/Doctor
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctores()
    {
        return await _context.Doctors.ToListAsync();
    }

    // POST: api/Doctor
    [HttpPost]
    public async Task<ActionResult<Doctor>> CrearDoctor([FromBody] Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDoctores), new { id = doctor.IdDoctor }, doctor);
    }
}