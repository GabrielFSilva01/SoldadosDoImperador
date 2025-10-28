using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoldadosDoImperador.Models
{
    
    public class TreinamentoParticipante
    {
        // --- Chave Estrangeira para Treinamento ---
        [Required]
        [Display(Name = "Treinamento")]
        public int TreinamentoId { get; set; }

        [ForeignKey(nameof(TreinamentoId))]
        public Treinamento Treinamento { get; set; } = null!;

        // --- Chave Estrangeira para Soldado ---
        [Required]
        [Display(Name = "Soldado Participante")]
        public int SoldadoId { get; set; }

        [ForeignKey(nameof(SoldadoId))]
        public Soldado Soldado { get; set; } = null!;
    }
}