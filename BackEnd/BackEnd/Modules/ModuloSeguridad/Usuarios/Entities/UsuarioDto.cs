namespace BackEnd.Modules.ModuloSeguridad.Usuarios.Entities
{
    public class UsuarioDto
    {
        public int userId { get; set; }
        public string userCode { get; set; }
        public string userName { get; set; }
        public string userUsername { get; set; }
        public int rolId { get; set; }
        public string userEmail { get; set; }
        public string userPhone { get; set; }
        public bool userStatus { get; set; }
    }
}
