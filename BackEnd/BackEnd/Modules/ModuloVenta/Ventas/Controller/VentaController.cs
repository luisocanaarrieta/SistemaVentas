using BackEnd.Modules.ModuloVenta.Ventas.Entities;
using BackEnd.Modules.ModuloVenta.Ventas.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Modules.ModuloVenta.Ventas.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;
        private readonly IConfiguration _config;

        public VentaController(IVentaService ventaService, IConfiguration config)
        {
            _ventaService = ventaService;
            _config = config;
        }

        [HttpPost("RegistrarVenta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrarVenta([FromBody] Venta venta)
        {
            try
            {
                var result = await _ventaService.RegistrarVenta(venta);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
