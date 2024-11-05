
using BackEnd.Modules.ModuloVenta.Ventas.Entities;

namespace BackEnd.Modules.ModuloVenta.Ventas.Interfaces
{
    public interface IVentaRepository
    {
        Task<int> InsertarCabeceraVenta(Venta venta);
        Task InsertarDetalleVenta(DetalleVenta venta);
        Task<List<VentaLista>> ListarVentas(VentaDto venta);
        Task<List<VentaDetalles>> ListarDetalleVenta(int saleId);
    }
}
