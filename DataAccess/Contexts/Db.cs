using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {
        public DbSet<Brans> Branslar { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<DoktorHasta> DoktorHastalar { get; set; }
        public DbSet<Hasta> Hastalar { get; set; }
        public DbSet<Klinik> Klinikler { get; set; }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Rol> Roller { get; set; }

        public Db(DbContextOptions options) : base(options)
        {
            
        }
    }
}
