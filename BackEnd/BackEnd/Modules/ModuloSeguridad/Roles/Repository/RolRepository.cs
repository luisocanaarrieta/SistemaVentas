using BackEnd.Modules.ModuloSeguridad.Roles.Entities;
using BackEnd.Modules.ModuloSeguridad.Roles.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackEnd.Modules.ModuloSeguridad.Roles.Repository
{
    public class RolRepository : IRolRepository
    {
        private readonly string _connectionString;

        public RolRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexion");
        }

        public async Task<List<Rol>> obtenerListaRoles()
        {
            List<Rol> lista = new List<Rol>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_ListarRoles", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var item = new Rol()
                            {
                                rolId = dr.GetInt32(dr.GetOrdinal("ROL_ID")),
                                rolName = dr.GetString(dr.GetOrdinal("ROL_NAME"))
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
