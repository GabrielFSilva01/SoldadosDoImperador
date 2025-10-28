using System;
using System.Collections.Generic; // Necessário para ICollection
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoldadosDoImperador.Models
{
  
    public enum StatusMissao
    {
        Pendente = 1,
        EmAndamento = 2,
        ConcluidaComSucesso = 3,
        ConcluidaComFalha = 4,
        Cancelada = 5
    }

    public class Missao
    {
       
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "O Nome da Missão é obrigatório.")]
        [StringLength(150, ErrorMessage = "O Nome deve ter no máximo 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Objetivo da Missão é obrigatório.")]
        [StringLength(500, ErrorMessage = "O Objetivo deve ter no máximo 500 caracteres.")]
        public string Objetivo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Status da Missão é obrigatório.")]
        public StatusMissao Status { get; set; }

        [Required(ErrorMessage = "A Data de Início é obrigatória.")]
        [DataType(DataType.DateTime)]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A Localização da Missão é obrigatória.")]
        [StringLength(200, ErrorMessage = "A Localização deve ter no máximo 200 caracteres.")]
        public string Localizacao { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de Fim ")]
        public DateTime? DataFim { get; set; }


        public ICollection<MissaoParticipante> Participantes { get; set; } = new List<MissaoParticipante>();
    }
}