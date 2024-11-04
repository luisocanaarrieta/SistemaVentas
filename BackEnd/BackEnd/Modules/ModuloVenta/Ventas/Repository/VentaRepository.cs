using BackEnd.Modules.ModuloVenta.Ventas.Entities;
using BackEnd.Modules.ModuloVenta.Ventas.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackEnd.Modules.ModuloVenta.Ventas.Repository
{
    public class VentaRepository : IVentaRepository
    {
        private readonly string _connectionString;

        public VentaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexion");
        }

        public async Task<int> InsertarCabeceraVenta(Venta venta)
        {
            int saleId = 0;
            decimal saleNet = Convert.ToDecimal(venta.totalTexto);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UspVentas_InsertarCabeceraVenta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@NUM_ID", SqlDbType.Int) { Value = 1 });
                    command.Parameters.Add(new SqlParameter("@NUM_NUMBER", SqlDbType.Int) { Value = 1});
                    command.Parameters.Add(new SqlParameter("@SALE_TYPE_PAYMENT", SqlDbType.VarChar) { Value  = venta.tipoPago });
                    command.Parameters.Add(new SqlParameter("@STATUS_ORDER_ID", SqlDbType.Int) { Value = 1 });
                    command.Parameters.Add(new SqlParameter("@SALE_NET", SqlDbType.Decimal) { Value = saleNet });
                    command.Parameters.Add(new SqlParameter("@LOG_USER_CREATE", SqlDbType.VarChar) { Value = venta.usuarioCrea });

                    var outputParameter = new SqlParameter("@SALE_ID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    await command.ExecuteNonQueryAsync();
                    saleId = (int)outputParameter.Value;
                }
            }
            return saleId;
        }

        public async Task InsertarDetalleVenta(DetalleVenta venta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UspVentas_InsertarDetalleVenta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@SALE_ID", SqlDbType.Int) { Value = venta.ventaId });
                    command.Parameters.Add(new SqlParameter("@PRODUCT_ID", SqlDbType.Int) { Value = venta.productId });
                    command.Parameters.Add(new SqlParameter("@SALE_DETAIL_QUANTITY", SqlDbType.Int) { Value = venta.quantity });
                    command.Parameters.Add(new SqlParameter("@SALE_DETAIL_NET", SqlDbType.Decimal) { Value = venta.Price });
                    command.Parameters.Add(new SqlParameter("@LOG_USER_CREATE", SqlDbType.VarChar) { Value = "ADMIN" });

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
