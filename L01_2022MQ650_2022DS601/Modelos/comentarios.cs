using System.ComponentModel.DataAnnotations;
namespace L01_2022MQ650_2022DS601.Modelos
{
    public class comentarios
    {
        [Key]
        public int ComentarioId { get; set; }
        public int PublicacionId { get; set; }
        public string ComentarioTexto { get; set; }
        public int UsuarioId { get; set; }
    }
}
