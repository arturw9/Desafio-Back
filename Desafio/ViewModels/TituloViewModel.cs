namespace Desafio.ViewModels
{
    public class TituloViewModel
    {
        public string NumeroTitulo { get; set; } = string.Empty;
        public string NomeDevedor { get; set; } = string.Empty;
        public string CpfDevedor { get; set; } = string.Empty;
        public decimal PercentualJuros { get; set; }
        public decimal PercentualMulta { get; set; }
        public List<ParcelaViewModel> Parcelas { get; set; } = new();
    }

    public class ParcelaViewModel
    {
        public int NumeroParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorParcela { get; set; }
    }
}