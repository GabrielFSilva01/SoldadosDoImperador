using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoldadosDoImperador.Models;

namespace SoldadosDoImperador.Models
{
   
    public enum TipoItem
    {
        Arma = 1,
        Equipamento = 2,
        Utilitario = 3
    }

    public class ItemBatalha
    {
       
        [Key]
        public int Id { get; set; }

     
        [Required(ErrorMessage = "O Nome do item é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Tipo de item é obrigatório.")]
        public TipoItem Tipo { get; set; }

    
        [Required(ErrorMessage = "O Peso do item é obrigatório.")]
        [Range(typeof(decimal), "0.0", "1000.0", ErrorMessage = "O Peso deve ser um valor positivo, entre {1} e {2}.")]
        [Column(TypeName = "decimal(10, 2)")] // Garante o tipo no banco
        public decimal Peso { get; set; }

      
        [StringLength(500, ErrorMessage = "A Especificação deve ter no máximo 500 caracteres.")]
        public string Especificacao { get; set; } = string.Empty;

    
        [Range(1, int.MaxValue, ErrorMessage = "O campo Soldado é obrigatório.")]
        public int SoldadoId { get; set; }

        
        [ForeignKey(nameof(SoldadoId))]
        public Soldado Soldado { get; set; } = null!;
    }
}