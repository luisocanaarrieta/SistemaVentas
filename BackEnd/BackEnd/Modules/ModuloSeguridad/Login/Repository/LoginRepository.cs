
using BackEnd.Modules.ModuloSeguridad.Login.Interfaces;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BackEnd.Modules.ModuloSeguridad.Login.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;

        public LoginRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexion");
        }


        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            Usuario user = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UspVentas_ValidarUsuarioLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar) { Value = usuario.userUserName });
                    command.Parameters.Add(new SqlParameter("@UserPassword", SqlDbType.VarChar) { Value = usuario.userPassword });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new Usuario
                            {
                                userCode = reader.IsDBNull(reader.GetOrdinal("USER_CODE")) ? null : reader.GetString(reader.GetOrdinal("USER_CODE")),
                                userName = reader.GetString(reader.GetOrdinal("USER_NAME")),
                                userUserName = reader.GetString(reader.GetOrdinal("USER_USERNAME")),
                                userRole = reader.GetInt32(reader.GetOrdinal("ROL_ID")),
                                userMail = reader.IsDBNull(reader.GetOrdinal("USER_MAIL")) ? null : reader.GetString(reader.GetOrdinal("USER_MAIL")),
                                userPhone = reader.IsDBNull(reader.GetOrdinal("USER_PHONE_NUMBER")) ? null : reader.GetString(reader.GetOrdinal("USER_PHONE_NUMBER")),
                                status = reader.GetBoolean(reader.GetOrdinal("USER_STATUS")),
                            };
                        }
                    }
                }
            }

            return user;
        }

    }
}
