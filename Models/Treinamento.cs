using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoldadosDoImperador.Models
{
    public class Treinamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome do Treinamento é obrigatório.")]
        [StringLength(150, ErrorMessage = "O Nome deve ter no máximo 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Área de Treinamento é obrigatória.")]
        [StringLength(100, ErrorMessage = "A Área deve ter no máximo 100 caracteres.")]
        [Display(Name = "Área de Especialização")]
        public string Area { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Duração do treinamento em horas é obrigatória.")]
        [Range(1, 1000, ErrorMessage = "A Duração deve ser positiva e no máximo {2} horas.")]
        [Display(Name = "Duração (Horas)")]
        public int DuracaoHoras { get; set; }

        [Required(ErrorMessage = "A Data de Realização é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data da Sessão")]
        public DateTime DataRealizacao { get; set; }

   
        public ICollection<TreinamentoParticipante> Participantes { get; set; } = new List<TreinamentoParticipante>();
    }
}
