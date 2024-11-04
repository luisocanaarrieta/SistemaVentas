using BackEnd.Modules.ModuloMantenimiento.Producto.Entities;
using BackEnd.Modules.ModuloMantenimiento.Producto.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Modules.ModuloMantenimiento.Producto.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly IConfiguration _config;

        public ProductoController(IProductoService productoService, IConfiguration config )
        {
            _productoService = productoService;
            _config = config;
        }

        [HttpGet("ListarProductos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarProductos()
        {
            try
            {
                var result = await _productoService.ListarProductos();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("InsertarProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertarProducto([FromBody] Productto usuario)
        {
            try
            {
                var result = await _productoService.InsertarProducto(usuario);

                return Ok(new { message = "OK" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("EliminarProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EliminarProducto([FromBody] int productId)
        {
            try
            {
                var result = await _productoService.EliminarProducto(productId);

                return Ok(new { message = "OK" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("ActualizarProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarProducto([FromBody] Productto usuario)
        {
            try
            {
                var result = await _productoService.ActualizarProducto(usuario);

                return Ok(new { message = "OK" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

   
}
