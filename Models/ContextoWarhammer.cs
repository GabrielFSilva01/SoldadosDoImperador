using Microsoft.EntityFrameworkCore;

using SoldadosDoImperador.Models;

namespace SoldadosDoImperador.Data 
{
    public class ContextoWarhammer : DbContext
    {
        public ContextoWarhammer(DbContextOptions<ContextoWarhammer> options) : base(options)
        {
        }

       
        public DbSet<Soldado> Soldados { get; set; } = null!;
        public DbSet<Ordem> Ordens { get; set; } = null!;
        public DbSet<ItemBatalha> ItensBatalha { get; set; } = null!; //
        public DbSet<Treinamento> Treinamentos { get; set; } = null!;
        public DbSet<Missao> Missoes { get; set; } = null!;

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            modelBuilder.Entity<Soldado>()
                .Property(s => s.Altura)
                .HasPrecision(6, 3);

            modelBuilder.Entity<Soldado>()
                .Property(s => s.Peso)
                .HasPrecision(6, 3);

          
            modelBuilder.Entity<ItemBatalha>()
                .Property(i => i.Peso)
                .HasPrecision(6, 3);

            
            base.OnModelCreating(modelBuilder);
        }
    }
}