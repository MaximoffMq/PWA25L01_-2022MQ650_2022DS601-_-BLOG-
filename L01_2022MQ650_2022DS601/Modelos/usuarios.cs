using System.ComponentModel.DataAnnotations;
namespace L01_2022MQ650_2022DS601.Modelos
{
    public class usuarios
    {
        [Key]

        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

    }
}
