
using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BackEnd.Modules.ModuloSeguridad.Usuarios.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexion");
        }

        public async Task<int> ValidateExistence(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UspVentas_ValidarUsuarioExiste", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@USUARIO", SqlDbType.VarChar) { Value = usuario.userUserName });
                    command.Parameters.Add(new SqlParameter("@CLAVE", SqlDbType.VarChar) { Value = usuario.userPassword });

                    var result = await command.ExecuteScalarAsync();

                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }



        public async Task<int> SaveUser(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UspVentas_InsertarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@USER_CODE", SqlDbType.VarChar) { Value = usuario.userCode });
                    command.Parameters.Add(new SqlParameter("@USER_NAME", SqlDbType.VarChar) { Value = usuario.userName });
                    command.Parameters.Add(new SqlParameter("@USER_USERNAME", SqlDbType.VarChar) { Value = usuario.userUserName });
                    command.Parameters.Add(new SqlParameter("@USER_PASSWORD", SqlDbType.VarChar) { Value = usuario.userPassword });
                    command.Parameters.Add(new SqlParameter("@ROL_ID", SqlDbType.Int) { Value = usuario.userRole });
                    command.Parameters.Add(new SqlParameter("@USER_MAIL", SqlDbType.VarChar) { Value = usuario.userMail });
                    command.Parameters.Add(new SqlParameter("@USER_PHONE_NUMBER", SqlDbType.VarChar) { Value = usuario.userPhone });
                    command.Parameters.Add(new SqlParameter("@LOG_USER_CREATE", SqlDbType.VarChar) { Value = usuario.usuarioCrea });


                    var newUserId = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(newUserId);
                }
            }
        }

        public async Task<List<UsuarioDto>> ListarUsuarios()
        {
            List<UsuarioDto> lista = new List<UsuarioDto>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_ListarUsuarios", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var item = new UsuarioDto()
                            {
                                userId = dr.GetInt32(dr.GetOrdinal("USER_ID")),
                                userCode = dr.GetString(dr.GetOrdinal("USER_CODE")),
                                userName = dr.GetString(dr.GetOrdinal("USER_NAME")),
                                userUsername = dr.GetString(dr.GetOrdinal("USER_USERNAME")),
                                rolId = dr.GetInt32(dr.GetOrdinal("ROL_ID")),
                                rolName = dr.GetString(dr.GetOrdinal("ROL_NAME")),
                                userEmail = dr.GetString(dr.GetOrdinal("USER_MAIL")),
                                userPhone = dr.GetString(dr.GetOrdinal("USER_PHONE_NUMBER")),
                                userStatus = dr.GetBoolean(dr.GetOrdinal("USER_STATUS"))
                            };

                            lista.Add(item);
                        }
                    }

                }
            }
            return lista;
        }

        public async Task<int> EliminarUsuario(int userId)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_EliminarUsuario", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@USER_ID", SqlDbType.Int) { Value = userId });

                    int result = await cmd.ExecuteNonQueryAsync();
                    return result;
                }
            }
        }

        public async Task<int> ActualizarUsuario(Usuario usuario)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_ActualizarUsuario", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@USER_ID", SqlDbType.Int) { Value = usuario.userId });
                    cmd.Parameters.Add(new SqlParameter("@USER_CODE", SqlDbType.VarChar){Value=usuario.userCode});
                    cmd.Parameters.Add(new SqlParameter("@USER_NAME",SqlDbType.VarChar){Value= usuario.userName});
                    cmd.Parameters.Add(new SqlParameter("@USER_USERNAME",SqlDbType.VarChar){Value= usuario.userUserName});
                    cmd.Parameters.Add(new SqlParameter("@USER_MAIL", SqlDbType.VarChar){Value=usuario.userMail});
                    cmd.Parameters.Add(new SqlParameter("@USER_PHONE_NUMBER",SqlDbType.VarChar){Value= usuario.userPhone});
                    cmd.Parameters.Add(new SqlParameter("@USER_STATUS", SqlDbType.Bit){Value=usuario.status});
                    cmd.Parameters.Add(new SqlParameter("@ROL_ID", SqlDbType.Int){Value=usuario.userRole});
                    cmd.Parameters.Add(new SqlParameter("@LOG_USER_UPDATE", SqlDbType.VarChar){Value=usuario.usuarioCrea });

                    int result = await cmd.ExecuteNonQueryAsync();
                    return result; 
                }
            }
        }
    }
}
