namespace BackEnd.Modules.ModuloVenta.Ventas.Entities
{
    public class Venta
    {
        public int numId { get; set; }
        public int numNumber { get; set; }
        public string tipoPago { get; set; }
        public int statusOrden { get; set; }
        public string totalTexto { get; set; }
        public string usuarioCrea { get; set; }
        public List<DetalleVenta> DetalleVenta { get; set; }
        public int ventaId { get; set; }
    }
}
