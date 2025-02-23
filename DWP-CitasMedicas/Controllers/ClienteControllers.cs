using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DWP_CitasMedicas.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace DWP_CitasMedicas.Controllers
{
    public class ClienteController : ControllerBase
    {
        private readonly DwpContext _context;

        public ClienteController(DwpContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Cliente cliente)
        {
            if (await _context.Clientes.AnyAsync(c => c.Correo == cliente.Correo))
                return BadRequest("El correo ya está registrado.");

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok("Cuenta creada con éxito.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Cliente loginData)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == loginData.Correo && c.Contraseña == loginData.Contraseña);
            if (cliente == null) return Unauthorized("Credenciales incorrectas.");
            return Ok(new { mensaje = "Inicio de sesión exitoso" });
        }

      
    }
}
