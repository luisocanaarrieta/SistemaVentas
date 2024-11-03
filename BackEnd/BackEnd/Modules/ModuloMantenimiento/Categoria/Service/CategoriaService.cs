using BackEnd.Modules.ModuloMantenimiento.Categoria.Entities;
using BackEnd.Modules.ModuloMantenimiento.Categoria.Interfaces;

namespace BackEnd.Modules.ModuloMantenimiento.Categoria.Service
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
       public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<List<CategoriaDto>> ListarCategoriaProductos()
        {
           return await _categoriaRepository.ListarCategoriaProductos();
        }
    }
}
