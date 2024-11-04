using BackEnd.Modules.ModuloMantenimiento.Producto.Entities;
using BackEnd.Modules.ModuloMantenimiento.Producto.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackEnd.Modules.ModuloMantenimiento.Producto.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexion");
        }

   

        public async Task<List<ProductoDto>> ListarProductos()
        {
            List<ProductoDto> lista = new List<ProductoDto>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_ListarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var item = new ProductoDto()
                            {
                                productId = dr.GetInt32(dr.GetOrdinal("PRODUCT_ID")),
                                productSku = dr.GetString(dr.GetOrdinal("PRODUCT_SKU")),
                                productName = dr.GetString(dr.GetOrdinal("PRODUCT_NAME")),
                                categoryId = dr.GetInt32(dr.GetOrdinal("CATEGORY_ID")),
                                categoryName = dr.GetString(dr.GetOrdinal("CATEGORY_NAME")),
                                productStock = dr.GetInt32(dr.GetOrdinal("PRODUCT_STOCK")),
                                productPrice = dr.GetDecimal(dr.GetOrdinal("PRODUCT_PRICE")),
                                productStatus = dr.GetBoolean(dr.GetOrdinal("PRODUCT_STATUS"))
                            };

                            lista.Add(item);
                        }
                    }

                }
            }
            return lista;
        }

        public async Task<int> ActualizarProducto(Productto producto)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_ActualizarProducto", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@PRODUCT_ID", SqlDbType.Int) { Value = producto.productId });
                    cmd.Parameters.Add(new SqlParameter("@PRODUCT_NAME", SqlDbType.VarChar) { Value = producto.productName });
                    cmd.Parameters.Add(new SqlParameter("@PRODUCT_STOCK", SqlDbType.Int) { Value = producto.productStock });
                    cmd.Parameters.Add(new SqlParameter("@PRODUCT_PRICE", SqlDbType.Decimal) { Value = producto.productPrice });
                    cmd.Parameters.Add(new SqlParameter("@PRODUCT_STATUS", SqlDbType.Bit) { Value = producto.productStatus });
                    cmd.Parameters.Add(new SqlParameter("@LOG_USER_UPDATE", SqlDbType.VarChar) { Value = producto.usuarioCrea });

                    int result = await cmd.ExecuteNonQueryAsync();
                    return result;
                }
            }
        }

        public async Task<int> EliminarProducto(int productId)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_EliminarProducto", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@PRODUCT_ID", SqlDbType.Int) { Value = productId });

                    int result = await cmd.ExecuteNonQueryAsync();
                    return result;
                }
            }
        }

        public async Task<int> InsertarProducto(Productto productto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UspVentas_InsertarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@PRODUCT_NAME", SqlDbType.VarChar) { Value = productto.productName });
                    command.Parameters.Add(new SqlParameter("@CATEGORY_ID", SqlDbType.Int) { Value = productto.categoryId });
                    command.Parameters.Add(new SqlParameter("@PRODUCT_STOCK", SqlDbType.Int) { Value = productto.productStock });
                    command.Parameters.Add(new SqlParameter("@PRODUCT_PRICE", SqlDbType.Decimal) { Value = productto.productPrice });
                    command.Parameters.Add(new SqlParameter("@LOG_USER_CREATE", SqlDbType.VarChar) { Value = productto.usuarioCrea });


                    var newUserId = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(newUserId);
                }
            }
        }
    }
}
