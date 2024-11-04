using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Interfaces;
using BackEnd.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Modules.ModuloSeguridad.Usuarios.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _config;
        public UsuarioController(IUsuarioService usuarioService, IConfiguration config)
        {
            _usuarioService = usuarioService;
            _config = config;
        }

        [HttpPost("SaveUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveUser([FromBody] Usuario usuario)
        {
            try
            {
                
                usuario.userPassword = Encriptar.EncriptarPassword(usuario.userPassword);

                var validateExistence = await _usuarioService.ValidateExistence(usuario);
                if (validateExistence > 1)
                {
                    return BadRequest(new { message = "El usuario " + usuario.userUserName + " Ya existe" });
                }

                await _usuarioService.SaveUser(usuario);


                return Ok(new { message = "Ok" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ListarUsuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarUsuarios()
        {
            try
            {
                var result = await _usuarioService.ListarUsuarios();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("EliminarUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EliminarUsuario([FromBody] int userId )
        {
            try
            {
                var result = await _usuarioService.EliminarUsuario(userId);

                return Ok(new { message = "OK" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("ActualizarUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var result = await _usuarioService.ActualizarUsuario(usuario);

                return Ok(new { message = "OK" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
