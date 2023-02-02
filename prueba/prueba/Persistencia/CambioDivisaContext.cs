using Microsoft.EntityFrameworkCore;
using prueba.Dominio;

namespace prueba.Persistencia
{
    public class CambioDivisaContext : DbContext
    {
        public CambioDivisaContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moneda>()
                    .HasOne(m => m.TipoDeCambioOrigen)
                    .WithOne(t => t.MonedaOrigen)
                    .HasForeignKey<TipoDeCambio>(m => m.idMonedaOrigen);

            modelBuilder.Entity<Moneda>()
                    .HasOne(m => m.TipoDeCambioDestino)
                    .WithOne(t => t.MonedaDestino)
                    .HasForeignKey<TipoDeCambio>(m => m.idMonedaDestino);

            modelBuilder.Entity<Operacion>()
                   .HasOne(m => m.TipoDeCambio)
                   .WithOne(t => t.Operacion)
                   .HasForeignKey<TipoDeCambio>(m => m.idOperacion);

            modelBuilder.Entity<Transaccion>()
                   .HasOne(m => m.TipoDeCambio)
                   .WithOne(t => t.Transaccion)
                   .HasForeignKey<TipoDeCambio>(m => m.idTipoDeCambio);

            modelBuilder.Entity<Consulta>()
                   .HasOne(m => m.TipoDeCambio)
                   .WithOne(t => t.Consulta)
                   .HasForeignKey<TipoDeCambio>(m => m.idTipoDeCambio);
        }

        public DbSet<Consulta> Consulta { get; set; }
        public DbSet<Moneda> Moneda { get; set; }
        public DbSet<Operacion> Operacion { get; set; }
        public DbSet<TipoDeCambio> TipoDeCambio { get; set; }
        public DbSet<Transaccion> Transaccion { get; set; }
    }
}
