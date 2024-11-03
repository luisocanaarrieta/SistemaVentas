using BackEnd.Modules.ModuloSeguridad.Login.Interfaces;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;
using BackEnd.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Modules.ModuloSeguridad.Login.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _usuarioService;
        private readonly IConfiguration _config;
        public LoginController(ILoginService usuarioService, IConfiguration config)
        {
            _usuarioService = usuarioService;
            _config = config;
        }


        [HttpPost("ValidateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateUser([FromBody] Usuario usuario)
        {
            //Validar usuario existe
            try
            {
                usuario.userPassword = Encriptar.EncriptarPassword(usuario.userPassword);

                var user = await _usuarioService.ValidateUser(usuario);
                if (user == null)
                {
                    return BadRequest(new { message = "Usuario o contrasena invalidos" });
                }
                //Declarar el token y llamar a la clase
                string tokenString = JwtConfiguration.GetToken(user, _config);
                return Ok(new { token = tokenString });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}


