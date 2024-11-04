using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Modules.ModuloSeguridad.Usuarios.Entities
{
    public class Usuario
    {
        public int? userId { get; set; }
        public string? userCode { get; set; }
        public string? userName { get; set; }
        public string? userUserName { get; set; }
        public string? userPassword { get; set; }
        public int userRole { get; set; }
        public string? userMail { get; set; }
        public string? userPhone { get; set; }
        public bool status { get; set; }
        public string? usuarioCrea { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string? usuarioActualiza { get; set; }
        public DateTime fechaActualiza { get; set; }
        public DateTime? ends { get; set; }
    }
}
