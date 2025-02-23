using DWP_CitasMedicas.Models;
using Microsoft.AspNetCore.Mvc;

namespace DWP_CitasMedicas.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IServicioEmail servicioEmail;

        public EmailController(IServicioEmail servicioEmail)
        {
            this.servicioEmail = servicioEmail;
        }

        [HttpPost]
        public async Task<ActionResult> Enviar(string email, string tema, string cuerpo)
        {
            await servicioEmail.EnviarEmail(email, tema, cuerpo);
            return Ok();
        }
    }
}
