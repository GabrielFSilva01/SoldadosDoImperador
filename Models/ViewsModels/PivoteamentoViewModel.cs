namespace SoldadosDoImperador.ViewModels
{
  
    public class PivoteamentoViewModel
    {
        
        public List<string> CabecalhosPatentes { get; set; } = new List<string>();

     
        public List<LinhaPivotCapitulo> LinhasCapitulo { get; set; } = new List<LinhaPivotCapitulo>();
    }

   
    public class LinhaPivotCapitulo
    {
        public string NomeCapitulo { get; set; }

       
        public Dictionary<string, int> ContagemPorPatente { get; set; } = new Dictionary<string, int>();
    }
}