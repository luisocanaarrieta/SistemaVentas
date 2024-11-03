
using BackEnd.Modules.ModuloMantenimiento.Categoria.Entities;

namespace BackEnd.Modules.ModuloMantenimiento.Categoria.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<CategoriaDto>> ListarCategoriaProductos();
    }
}
