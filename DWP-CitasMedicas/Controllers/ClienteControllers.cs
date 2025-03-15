using DWP_CitasMedicas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly DwpContext _context;

    public ClienteController(DwpContext context)
    {
        _context = context;
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> RegistrarCliente([FromBody] Cliente cliente)
    {
        // Verifica si el correo ya está registrado
        if (_context.Clientes.Any(c => c.Correo == cliente.Correo))
        {
            Console.WriteLine($"Correo ya registrado: {cliente.Correo}"); // Agrega esto
            return BadRequest("El correo ya está registrado.");
        }

        // Guardar el cliente en la base de datos
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Cliente registrado exitosamente." });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> IniciarSesion([FromBody] LoginRequest request)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == request.Correo);
        if (cliente == null || cliente.Contraseña != request.Contraseña) // Comparación en texto plano
        {
            return Unauthorized("Correo o contraseña incorrectos.");
        }

        return Ok(new { Message = "Inicio de sesión exitoso.", ClienteId = cliente.IdCliente });
    }
}

public class LoginRequest
{
    public string Correo { get; set; } = null!;
    public string Contraseña { get; set; } = null!;
}