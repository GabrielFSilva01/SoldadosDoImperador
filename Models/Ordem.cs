using System;

namespace SoldadosDoImperador.Models
{
    public enum StatusOrdem
    {
        Pendente = 1,
        EmExecucao = 2,
        Concluida = 3,
        Expirada = 4
    }

    public class Ordem
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty; 
        public string Descricao { get; set; } = string.Empty;
        public StatusOrdem Status { get; set; } 
        public DateTime DataEmissao { get; set; }
        public DateTime PrazoFinal { get; set; } 

     
        public int SoldadoId { get; set; }

        public Soldado Soldado { get; set; } = null!; 
    }
}