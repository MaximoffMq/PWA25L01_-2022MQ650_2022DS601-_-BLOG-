using System.ComponentModel.DataAnnotations;

namespace L01_2022MQ650_2022DS601.Modelos
{
    public class calificaciones
    {
        [Key]
        public int CalificacionId { get; set; }
        public int PublicacionId { get; set; }
        public int UsuarioId { get; set; }
        public int Calificacion { get; set; }
    }
}
