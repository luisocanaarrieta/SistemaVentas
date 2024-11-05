namespace BackEnd.Modules.ModuloMantenimiento.Producto.Entities
{
    public class Productto
    {
        public int productId { get; set; }
        public string productSku { get; set; }
        public string productName { get; set; }
        public int categoryId { get; set; }
        public string? categoryName { get; set; }
        public int productStock { get; set; }
        public decimal productPrice { get; set; }
        public bool productStatus { get; set; }
        public string usuarioCrea { get; set; }
    }
}
