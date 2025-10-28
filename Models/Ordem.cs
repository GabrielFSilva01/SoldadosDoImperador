using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoldadosDoImperador.Models
{
    // A enumeração StatusOrdem
    public enum StatusOrdem
    {
        Pendente = 1,
        EmExecucao = 2,
        Concluida = 3,
        Expirada = 4
    }

    public class Ordem
    {
        // 1. Chave Primária
        [Key]
        public int Id { get; set; }

        // --- Propriedades de Dados ---

        // 2. Título
        [Required(ErrorMessage = "O Título da Ordem é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        // 3. Descrição
        [Required(ErrorMessage = "A Descrição da Ordem é obrigatória.")]
        [StringLength(1000, ErrorMessage = "A Descrição deve ter no máximo 1000 caracteres.")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; } = string.Empty;

        // 4. Status (Enum)
        [Required(ErrorMessage = "O Status da Ordem é obrigatório.")]
        public StatusOrdem Status { get; set; }

        // 5. Data de Emissão
        [Required(ErrorMessage = "A Data de Emissão é obrigatória.")]
        [DataType(DataType.DateTime)]
        public DateTime DataEmissao { get; set; }

        // 6. Prazo Final
        [Required(ErrorMessage = "O Prazo Final é obrigatório.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Prazo Final")]
        public DateTime PrazoFinal { get; set; }

        // --- Chave Estrangeira e Propriedade de Navegação ---

        // 7. Chave Estrangeira (O Alvo da Ordem)
        // [Range] é a validação correta para FKs de dropdown.
        [Range(1, int.MaxValue, ErrorMessage = "O campo Soldado Alvo é obrigatório.")]
        [Display(Name = "Soldado Alvo")]
        public int SoldadoId { get; set; }

        // 8. Propriedade de Navegação
        // --- CORREÇÃO APLICADA AQUI ---
        // O [Required] foi REMOVIDO desta propriedade de navegação.
        [ForeignKey(nameof(SoldadoId))]
        public Soldado Soldado { get; set; } = null!;
    }
}