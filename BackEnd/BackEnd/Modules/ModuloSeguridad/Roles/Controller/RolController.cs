using BackEnd.Modules.ModuloSeguridad.Roles.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Modules.ModuloSeguridad.Roles.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet("obtenerListaRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> obtenerListaRoles()
        {
            try
            {
                var result = await _rolService.obtenerListaRoles();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
