using L01_2022MQ650_2022DS601.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace PracticaApi.Modelos
{
    public class blogDBcontext : DbContext
    {
        public blogDBcontext(DbContextOptions<blogDBcontext> options) : base(options)
        {
        }

        public DbSet<roles> roles { get; set; }

        public DbSet<calificaciones> calificaciones { get; set; }

        public DbSet<comentarios> comentarios { get; set; }

        public DbSet<publicaciones> publicaciones { get; set; }

        public DbSet<usuarios> usuarios { get; set; }

    }
}
