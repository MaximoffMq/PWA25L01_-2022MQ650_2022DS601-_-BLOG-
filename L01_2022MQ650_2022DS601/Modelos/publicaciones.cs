using System.ComponentModel.DataAnnotations;
namespace L01_2022MQ650_2022DS601.Modelos
{
    public class publicaciones
    {
        [Key]

        public int PublicacionId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }



    }
}
