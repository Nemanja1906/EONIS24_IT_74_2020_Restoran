using Microsoft.EntityFrameworkCore;
using System.Configuration;



namespace Restoran.Entities
{
    public class RestoranContext : DbContext
    {

        private readonly IConfiguration configuration;

        public RestoranContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }    

        public DbSet<Porudzbina> Porudzbina { get; set; } = default!;
         public DbSet<Proizvod>? Proizvod { get; set; }
        public DbSet<StavkaPorudzbine>? StavkaPorudzbine { get; set; }
        public DbSet<Musterija>? Musterija { get; set; }
        public DbSet<Zaposleni>? Zaposleni { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("RestoranDB"));
        }

        
    }
}
