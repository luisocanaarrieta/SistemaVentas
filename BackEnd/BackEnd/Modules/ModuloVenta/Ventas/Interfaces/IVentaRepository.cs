
using BackEnd.Modules.ModuloVenta.Ventas.Entities;

namespace BackEnd.Modules.ModuloVenta.Ventas.Interfaces
{
    public interface IVentaRepository
    {
        Task<int> InsertarCabeceraVenta(Venta venta);
        Task InsertarDetalleVenta(DetalleVenta venta);

    }
}
