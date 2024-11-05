using BackEnd.Modules.ModuloVenta.Ventas.Entities;

namespace BackEnd.Modules.ModuloVenta.Ventas.Interfaces
{
    public interface IVentaService
    {
        Task<int> RegistrarVenta(Venta venta);
        Task<List<VentaLista>> ListarVentas(VentaDto venta);
        Task<List<VentaLista>> ObtenerVentasConDetalle(VentaDto venta);
    }
}
