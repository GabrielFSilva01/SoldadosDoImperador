using System;

namespace SoldadosDoImperador.Models
{
    public class Treinamento
    {
        public int Id { get; set; }

   
        public string Nome { get; set; } = string.Empty; 
        public string Area { get; set; } = string.Empty; 
        public int DuracaoHoras { get; set; }
        public DateTime DataRealizacao { get; set; }

      
        public int SoldadoId { get; set; }

      
        public Soldado Soldado { get; set; } = null!; 
    }
}