using Microsoft.EntityFrameworkCore;
using MotelAPI.Entities;
namespace MotelAPI.Data
{
    public class MotelDbContext : DbContext
    {
        public MotelDbContext(DbContextOptions<MotelDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoSuite> TiposSuite { get; set; }
        public DbSet<Motel> Moteis { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany()
                .HasForeignKey(r => r.ClienteId);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.TipoSuite)
                .WithMany()
                .HasForeignKey(r => r.TipoSuiteId);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Motel)
                .WithMany() 
                .HasForeignKey(r => r.MotelId);
        }
    }
}
