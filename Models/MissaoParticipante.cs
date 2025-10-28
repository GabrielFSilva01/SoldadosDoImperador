using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoldadosDoImperador.Models
{
    // Esta é a nossa tabela de junção
    public class MissaoParticipante
    {
        // --- Chave Estrangeira para Missao ---
        [Required]
        [Display(Name = "Missão")]
        public int MissaoId { get; set; }

        [ForeignKey(nameof(MissaoId))]
        public Missao Missao { get; set; } = null!;

        // --- Chave Estrangeira para Soldado ---
        [Required]
        [Display(Name = "Soldado Participante")]
        public int SoldadoId { get; set; }

        [ForeignKey(nameof(SoldadoId))]
        public Soldado Soldado { get; set; } = null!;
    }
}