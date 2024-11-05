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

    public class VentaLista
    {
        public int saleId { get; set; }
        public string numero { get; set; }
        public DateTime saleDate { get; set; }
        public string statusDescripcion { get; set; }
        public string tipoPagoDescripcion { get; set; }
        public decimal saleNet { get; set; }
        public List<VentaDetalles> VentaDetalles { get; set; }

        

    }

    public class VentaDetalles
    {
        public int productId { get; set; }
        public string productSku { get; set; }
        public string productName { get; set; }
        public int productQuantity { get; set; }
        public decimal saleNet { get; set; }
        public decimal total { get; set; }

    }

}
