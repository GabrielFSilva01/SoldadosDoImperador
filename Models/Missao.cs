using System;

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

        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Objetivo { get; set; } = string.Empty;
        public StatusMissao Status { get; set; }
        public DateTime DataInicio { get; set; }
        public string Localizacao { get; set; } = string.Empty;

        public DateTime? DataFim { get; set; }

       
        public int SoldadoId { get; set; }

      
        public Soldado Soldado { get; set; } = null!; 
    }
}