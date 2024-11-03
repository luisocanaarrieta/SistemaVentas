using BackEnd.Modules.ModuloMantenimiento.Categoria.Entities;

namespace BackEnd.Modules.ModuloMantenimiento.Categoria.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> ListarCategoriaProductos();

    }
}
