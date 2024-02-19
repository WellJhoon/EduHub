using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Interfaces;
using static ProyectoFinal.DTOs.UsuarioDTO;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidationsManager _validationsManager;

        public UserController(IUserService userService, IValidationsManager validationsManager)
        {
            _userService = userService;
            _validationsManager = validationsManager;

        }

        [HttpPost("RegistrarUsuarios")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequestDto registerUsuario)
        {
            var validation = await _validationsManager.ValidateAsync(registerUsuario);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var emailExist = await _validationsManager.ValidateEmailExistAsync(registerUsuario.CorreoElectronico);

            if (emailExist)
            {
                return BadRequest("Ya existe un usuario creado con este email");
            }

            try
            {
                await _userService.RegisterUser(registerUsuario);
                return Ok("Usuario registrado exitosamente.");
 
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpPost("LoguearUsuarios")]
        public async Task<IActionResult> LoginUser(LoginUserRequestDto loginUsuario)
        {
            var validation = await _validationsManager.ValidateAsync(loginUsuario);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var emailExist = await _validationsManager.ValidateEmailExistAsync(loginUsuario.CorreoElectronico);

            if (!emailExist)
            {
                return BadRequest("No existe un usuario creado con este email");
            }

            try
            {
                var (result, token) = await _userService.LoginUser(loginUsuario);

                if(result)
                {

                    return Ok(new { Token = token });

                }

                else return BadRequest(new { Result = token});
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

    }
}
