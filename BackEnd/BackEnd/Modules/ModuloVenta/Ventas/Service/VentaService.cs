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
        public async Task<int> RegistrarVenta(Venta venta)
        {
            int ventaId;

            ventaId = await _ventaRepository.InsertarCabeceraVenta(venta);

            foreach (var detalle in venta.DetalleVenta)
            {
                detalle.ventaId = ventaId;
                await _ventaRepository.InsertarDetalleVenta(detalle);
            }
   
            return ventaId;
        }

    }
}
