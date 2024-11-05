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

                return Ok(new { message = "OK", resultado = result });


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ListarVentas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarVentas([FromBody]  VentaDto venta)
        {
            try
            {
                var result = await _ventaService.ListarVentas(venta);

                return Ok(new { message = "OK", resultado = result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("ObtenerVentasConDetalle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerVentasConDetalle([FromBody] VentaDto venta)
        {
            try
            {
                var result = await _ventaService.ObtenerVentasConDetalle(venta);

                return Ok(new { message = "OK", resultado = result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
