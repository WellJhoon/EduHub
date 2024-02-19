using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Services.Email;
using ProyectoFinal.Models;
using Microsoft.AspNetCore.Authorization;


namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmail _emailService;
        public EmailController(IEmail emailService)
        {
            _emailService = emailService;
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult SendEmail(EmailDto request)
        {
            _emailService.SendEmail(request);
            return Ok();
        }
    }
}
