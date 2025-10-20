using System;

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
        public int Id { get; set; }

        
        public string Nome { get; set; } = string.Empty;
        public TipoItem Tipo { get; set; }
        public decimal Peso { get; set; }
        public string Especificacao { get; set; } = string.Empty;

      
        public int SoldadoId { get; set; }

        
        public Soldado Soldado { get; set; } = null!;
    }
}