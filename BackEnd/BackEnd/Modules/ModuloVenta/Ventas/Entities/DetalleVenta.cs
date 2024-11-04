namespace BackEnd.Modules.ModuloVenta.Ventas.Entities
{
    public class DetalleVenta
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
        public decimal Price { get; set; }
        public  int ventaId { get; set; }
    }
}
