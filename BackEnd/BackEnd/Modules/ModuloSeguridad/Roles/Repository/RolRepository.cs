using BackEnd.Modules.ModuloSeguridad.Roles.Entities;
using BackEnd.Modules.ModuloSeguridad.Roles.Interfaces;
using BackEnd.Modules.ModuloVenta.Ventas.Entities;
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

        public async Task<List<ModuloXRol>> ModuloXRol(int rolId)
        {
            List<ModuloXRol> lista = new List<ModuloXRol>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_ModuloXRol", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@rolId", SqlDbType.Int) { Value = rolId });

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var item = new ModuloXRol()
                            {
                                rolName = dr.GetString(dr.GetOrdinal("ROL_NAME")),
                                moduloId = dr.GetInt32(dr.GetOrdinal("MODULE_ID")),
                                moduloName = dr.GetString(dr.GetOrdinal("MODULE_NAME")),
                                moduloIcon = dr.GetString(dr.GetOrdinal("MODULE_ICON")),
                                moduloRoute = dr.GetString(dr.GetOrdinal("MODULE_ROUTE")),
                            };

                            lista.Add(item);
                        }
                    }

                }
            }
            return lista;
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
