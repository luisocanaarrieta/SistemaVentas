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
    }

   
}
