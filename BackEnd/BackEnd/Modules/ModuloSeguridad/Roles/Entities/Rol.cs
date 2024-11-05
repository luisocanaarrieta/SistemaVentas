namespace BackEnd.Modules.ModuloSeguridad.Roles.Entities
{
    public class Rol
    {
        public int rolId { get; set; }
        public string rolName { get; set; }
    }
    public class ModuloXRol
    {
        public string rolName { get; set; }
        public int moduloId { get; set; }
        public string moduloName { get; set; }
        public string moduloIcon { get; set; }
        public string moduloRoute { get; set; }
    }
}
