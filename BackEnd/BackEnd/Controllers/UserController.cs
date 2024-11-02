using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackEnd.Context;
using BackEnd.Models;
using BackEnd.Domain.IServices;
using BackEnd.Utils;
using BackEnd.Services;
using BackEnd.DTO.Generics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usuarioService;
        private readonly IConfiguration _config;
        public UserController(IUserService usuarioService, IConfiguration config)
        {
            _usuarioService = usuarioService;
            _config = config;
        }

        [HttpPost("SaveUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveUser([FromBody] Users usuario)
        {
            try
            {
                var validateExistence = await _usuarioService.ValidateExistence(usuario);
                if (validateExistence)
                {
                    return BadRequest(new { message = "El usuario " + usuario.USER_NAME + " Ya existe" });
                }
                usuario.USER_PASSWORD = Encriptar.EncriptarPassword(usuario.USER_PASSWORD);
                await _usuarioService.SaveUser(usuario);

                return Ok(new { message = "Usuario Registrado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ValidateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateUser([FromBody] Users usuario)
        {
            //Validar usuario existe
            try
            {
                usuario.USER_PASSWORD = Encriptar.EncriptarPassword(usuario.USER_PASSWORD);

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

        [HttpPut("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword change)
        {
            try
            {

                var identity = HttpContext.User.Identity as ClaimsIdentity;

                int idUsuario = JwtConfiguration.GetTokenIdUsuario(identity);
                string passwordEncriptado = Encriptar.EncriptarPassword(change.PREVIOUS_KEY);
                var usuario = await _usuarioService.ValidatePassword(idUsuario, passwordEncriptado);
                if (usuario == null)
                {
                    return BadRequest(new { message = "La contraseña es incorrecta" });
                }
                else
                {
                    usuario.USER_PASSWORD = Encriptar.EncriptarPassword(change.NEW_KEY);
                    usuario.LOG_USER_UPDATE = "Admin";
                    usuario.LOG_DATE_UPDATE = DateTime.Now;
                    await _usuarioService.UpdatePassword(usuario);
                    return Ok(new { message = "La clave fue cambiada con exito" });
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
