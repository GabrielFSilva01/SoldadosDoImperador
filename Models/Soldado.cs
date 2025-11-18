using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoldadosDoImperador.Models
{
    public enum Patente
    {
        Recruta,
        Veterano,
        Sargento,
        Tenente,
        Capitao,
        MestreDeCapitulo,
        Primarca
    }

    public enum Capitulo
    {
        Ultramarines,
        BloodAngels,
        DarkAngels,
        SpaceWolves,
        IronHands,
        Salamanders,
        RavenGuard,
        WhiteScars,
        ImperialFists
    }

    public class Soldado
    {
        [Key]
        public int Id { get; set; }
      [Required(ErrorMessage = "O nome do Soldado é obrigatório.")]
        [StringLength(120, ErrorMessage = "O Nome deve ter no máximo 120 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Capítulo do Soldado é obrigatório.")]
        public Capitulo Capitulo { get; set; }

        [Required(ErrorMessage = "A Patente do Soldado é obrigatória.")]
        public Patente Patente { get; set; }

       
        [Required(ErrorMessage = "A Altura é obrigatória.")]
        [Range(typeof(decimal), "2.50", "4.50", ErrorMessage = "A Altura de um Astartes deve estar entre {1}m e {2}m.")]
        [Column(TypeName = "decimal(3, 2)")]
        public decimal Altura { get; set; }

        [Required(ErrorMessage = "O Peso é obrigatório.")]
        [Range(typeof(decimal), "200.0", "500.0", ErrorMessage = "O Peso Corporal de um Astartes deve estar entre {1}kg e {2}kg.")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Peso { get; set; }


 
        public ICollection<ItemBatalha> ItensDeBatalha { get; set; } = new List<ItemBatalha>();
        public ICollection<Ordem> OrdensRecebidas { get; set; } = new List<Ordem>();

      

    
        public ICollection<MissaoParticipante> MissoesParticipadas { get; set; } = new List<MissaoParticipante>();

        public ICollection<TreinamentoParticipante> TreinamentosParticipados { get; set; } = new List<TreinamentoParticipante>();
    }
}
