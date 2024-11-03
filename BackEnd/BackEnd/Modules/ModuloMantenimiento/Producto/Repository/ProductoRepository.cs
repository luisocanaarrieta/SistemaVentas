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
    }
}
