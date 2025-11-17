using Microsoft.EntityFrameworkCore;
using SoldadosDoImperador.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Para IdentityDbContext
using SoldadosDoImperador.Areas.Identity.Data; // Para ApplicationUser

namespace SoldadosDoImperador.Data
{
    // O Contexto Mestre: herda de IdentityDbContext<ApplicationUser> E tem seus DbSets
    public class ContextoWarhammer : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Soldado> Soldados { get; set; }
        public DbSet<Missao> Missoes { get; set; }
        public DbSet<ItemBatalha> ItensBatalha { get; set; }
        public DbSet<Ordem> Ordens { get; set; }
        public DbSet<Treinamento> Treinamentos { get; set; }

        // Seus DbSets de Tabela de Junção (2)
        public DbSet<MissaoParticipante> MissoesParticipantes { get; set; }
        public DbSet<TreinamentoParticipante> TreinamentosParticipantes { get; set; }

        public ContextoWarhammer(DbContextOptions<ContextoWarhammer> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Essencial para o Identity criar suas tabelas
            base.OnModelCreating(modelBuilder);

            // Suas configurações M-N (MissaoParticipante, TreinamentoParticipante)...
            modelBuilder.Entity<MissaoParticipante>().HasKey(mp => new { mp.MissaoId, mp.SoldadoId });
            modelBuilder.Entity<MissaoParticipante>().HasOne(mp => mp.Soldado).WithMany(s => s.MissoesParticipadas).HasForeignKey(mp => mp.SoldadoId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MissaoParticipante>().HasOne(mp => mp.Missao).WithMany(m => m.Participantes).HasForeignKey(mp => mp.MissaoId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TreinamentoParticipante>().HasKey(tp => new { tp.TreinamentoId, tp.SoldadoId });
            modelBuilder.Entity<TreinamentoParticipante>().HasOne(tp => tp.Soldado).WithMany(s => s.TreinamentosParticipados).HasForeignKey(tp => tp.SoldadoId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TreinamentoParticipante>().HasOne(tp => tp.Treinamento).WithMany(t => t.Participantes).HasForeignKey(tp => tp.TreinamentoId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}