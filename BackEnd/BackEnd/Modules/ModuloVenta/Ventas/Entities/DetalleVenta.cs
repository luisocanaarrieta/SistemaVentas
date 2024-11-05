namespace BackEnd.Modules.ModuloVenta.Ventas.Entities
{
    public class DetalleVenta
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public int cantidad { get; set; }
        public decimal precioTexto { get; set; }
        public  int ventaId { get; set; }
        public string? usuarioCrea { get; set; }
    }
}
