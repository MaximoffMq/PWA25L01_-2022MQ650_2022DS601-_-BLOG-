using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace PracticaApi.Modelos
{
    public class blogDBcontext : DbContext
    {
        public blogDBcontext(DbContextOptions<blogDBcontext> options) : base(options)
        {
        }

        public DbSet<autor> autor { get; set; }


    }
}
