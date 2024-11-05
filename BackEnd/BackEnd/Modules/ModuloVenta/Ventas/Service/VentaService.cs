using BackEnd.Modules.ModuloVenta.Ventas.Entities;
using BackEnd.Modules.ModuloVenta.Ventas.Interfaces;

namespace BackEnd.Modules.ModuloVenta.Ventas.Service
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;

        public VentaService(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        public async Task<List<VentaLista>> ListarVentas(VentaDto venta)
        {
            return await _ventaRepository.ListarVentas(venta);
        }

        public async Task<int> RegistrarVenta(Venta venta)
        {
            int ventaId;

            ventaId = await _ventaRepository.InsertarCabeceraVenta(venta);

            foreach (var detalle in venta.DetalleVenta)
            {
                detalle.ventaId = ventaId;
                detalle.usuarioCrea = venta.usuarioCrea;
                await _ventaRepository.InsertarDetalleVenta(detalle);
            }

            return ventaId;
        }

        public async Task<List<VentaLista>> ObtenerVentasConDetalle(VentaDto venta)
        {
            List<VentaLista> ventas = await ListarVentas(venta);

            foreach (var ventaCabecera in ventas)
            {
                ventaCabecera.VentaDetalles = await _ventaRepository.ListarDetalleVenta(ventaCabecera.saleId);
            }

            return ventas;
        }

        public async Task<List<EstadosDto>> ListarEstadosReparto()
        {
            return await _ventaRepository.ListarEstadosReparto();
        }

        public async Task<int> CambiarEstadoVenta(int saleId, int statusOrderId)
        {
            return await _ventaRepository.CambiarEstadoVenta(saleId, statusOrderId);
        }
    }
}
