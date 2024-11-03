using BackEnd.Modules.ModuloMantenimiento.Categoria.Entities;
using BackEnd.Modules.ModuloMantenimiento.Categoria.Interfaces;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackEnd.Modules.ModuloMantenimiento.Categoria.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string _connectionString;

        public CategoriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexion");
        }

        public async Task<List<CategoriaDto>> ListarCategoriaProductos()
        {
            List<CategoriaDto> lista = new List<CategoriaDto>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_ListarCategoriaProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var item = new CategoriaDto()
                            {
                                categoriaId = dr.GetInt32(dr.GetOrdinal("CATEGORY_ID")),
                                categoriaName = dr.GetString(dr.GetOrdinal("CATEGORY_NAME")),
                            };

                            lista.Add(item);
                        }
                    }
                }
            }
            return lista;
        }

    }
}
