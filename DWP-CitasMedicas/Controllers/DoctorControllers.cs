using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DWP_CitasMedicas.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DWP_CitasMedicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DwpContext _context;

        public DoctorController(DwpContext context)
        {
            _context = context;
        }

        // GET: api/Doctor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _context.Doctors.ToListAsync();
        }
    }
}
