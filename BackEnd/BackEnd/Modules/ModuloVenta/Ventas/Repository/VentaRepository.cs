using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;
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
                    command.Parameters.Add(new SqlParameter("@SALE_DETAIL_QUANTITY", SqlDbType.Int) { Value = venta.cantidad });
                    command.Parameters.Add(new SqlParameter("@SALE_DETAIL_NET", SqlDbType.Decimal) { Value = venta.precioTexto });
                    command.Parameters.Add(new SqlParameter("@LOG_USER_CREATE", SqlDbType.VarChar) { Value = venta.usuarioCrea });

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<VentaLista>> ListarVentas(VentaDto venta)
        {
            List<VentaLista> lista = new List<VentaLista>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_ListarVentas", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fechaInicio", SqlDbType.VarChar) { Value = venta.fechaInicio });
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", SqlDbType.VarChar) { Value = venta.fechaFin });
                    cmd.Parameters.Add(new SqlParameter("@filtroBuscar", SqlDbType.VarChar) { Value = venta.filtroBuscar });
                    cmd.Parameters.Add(new SqlParameter("@numero", SqlDbType.VarChar) { Value = venta.numero });


                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var item = new VentaLista()
                            {
                                saleId = dr.GetInt32(dr.GetOrdinal("SALE_ID")),
                                numero = dr.GetString(dr.GetOrdinal("NUMERO")),
                                saleDate = dr.GetDateTime(dr.GetOrdinal("SALE_DATE")),
                                statusOrderId = dr.GetInt32(dr.GetOrdinal("STATUS_ORDER_ID")),
                                statusDescripcion = dr.GetString(dr.GetOrdinal("STATUS_ORDER_NAME")),
                                tipoPagoDescripcion = dr.GetString(dr.GetOrdinal("SALE_TYPE_PAYMENT")),
                                saleNet = dr.GetDecimal(dr.GetOrdinal("SALE_NET")),

                            };

                            lista.Add(item);
                        }
                    }

                }
            }
            return lista;
        }


        public async Task<List<VentaDetalles>> ListarDetalleVenta(int saleId)
        {
            List<VentaDetalles> detalles = new List<VentaDetalles>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_ListarDetalleVenta", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@saleId", SqlDbType.Int) { Value = saleId });

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var detalle = new VentaDetalles()
                            {
                                productId = dr.GetInt32(dr.GetOrdinal("PRODUCT_ID")),
                                productSku = dr.GetString(dr.GetOrdinal("PRODUCT_SKU")),
                                productName = dr.GetString(dr.GetOrdinal("PRODUCT_NAME")),
                                productQuantity = dr.GetInt32(dr.GetOrdinal("SALE_DETAIL_QUANTITY")),
                                saleNet = dr.GetDecimal(dr.GetOrdinal("SALE_DETAIL_NET")),
                                total = dr.GetDecimal(dr.GetOrdinal("TOTAL"))
                            };

                            detalles.Add(detalle);
                        }
                    }
                }
            }

            return detalles;
        }

        public async Task<List<EstadosDto>> ListarEstadosReparto()
        {
            List<EstadosDto> lista = new List<EstadosDto>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_ListarEstadosReparto", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var item = new EstadosDto()
                            {
                                statusVentaId = dr.GetInt32(dr.GetOrdinal("STATUS_ORDER_ID")),
                                statusVentaName = dr.GetString(dr.GetOrdinal("STATUS_ORDER_NAME")),
                                statusVentaDescripcion = dr.GetString(dr.GetOrdinal("STATUS_ORDER_DESC")),

                            };

                            lista.Add(item);
                        }
                    }

                }
            }
            return lista;
        }

        public async Task<int> CambiarEstadoVenta(int saleId, int statusOrderId)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                await sql.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UspVentas_CambiarEstadoVenta", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@saleId", SqlDbType.Int) { Value = saleId });
                    cmd.Parameters.Add(new SqlParameter("@statusOrderId", SqlDbType.Int) { Value = statusOrderId });

                    int result = await cmd.ExecuteNonQueryAsync();
                    return result;
                }
            }
        }
    }
}
