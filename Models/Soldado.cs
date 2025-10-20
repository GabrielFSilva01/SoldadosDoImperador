using System.Collections.Generic; // Necessário para ICollection

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

        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

       
        public Capitulo Capitulo { get; set; }
        public Patente Patente { get; set; }

        public decimal Altura { get; set; }
        public decimal Peso { get; set; }

       

        public ICollection<ItemBatalha> ItensDeBatalha { get; set; } = new List<ItemBatalha>();
        public ICollection<Missao> Missoes { get; set; } = new List<Missao>();
        public ICollection<Treinamento> Treinamentos { get; set; } = new List<Treinamento>();
        public ICollection<Ordem> OrdensRecebidas { get; set; } = new List<Ordem>();
    }
}