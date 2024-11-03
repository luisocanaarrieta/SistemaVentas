using BackEnd.Modules.ModuloMantenimiento.Categoria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Modules.ModuloMantenimiento.Categoria.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;   
        }

        [HttpGet("ListarCategoriaProductos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarCategoriaProductos()
        {
            try
            {
                var result = await _categoriaService.ListarCategoriaProductos();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
