using System.ComponentModel.DataAnnotations;
namespace L01_2022MQ650_2022DS601.Modelos
{
    public class roles
    {
        [Key]
        public int RolId { get; set; }
        public string Rol { get; set; }
    }
}
