using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DWP_CitasMedicas.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DWP_CitasMedicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly DwpContext _context;

        public AgendaController(DwpContext context)
        {
            _context = context;
        }

        // GET: api/Agenda/{idDoctor}
        [HttpGet("{idDoctor}")]
        public async Task<ActionResult<IEnumerable<Agenda>>> GetAgendaByDoctor(int idDoctor)
        {
            var agendas = await _context.Agenda
                .Where(a => a.IdDoctor == idDoctor)
                .ToListAsync();

            if (agendas == null || agendas.Count == 0)
            {
                return NotFound("No hay agendas disponibles para este doctor.");
            }

            return Ok(agendas);
        }
    }
}
