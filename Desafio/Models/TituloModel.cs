using System.ComponentModel.DataAnnotations;

namespace Desafio.Models
{
    public class TituloModel
    {
        [Key]
        public int Id { get; set; }
        public string NumeroTitulo { get; set; } = string.Empty;
        public string NomeDevedor { get; set; } = string.Empty;
        public string CpfDevedor { get; set; } = string.Empty;
        public decimal PercentualJuros { get; set; }
        public decimal PercentualMulta { get; set; }
        public List<ParcelaModel> Parcelas { get; set; } = new();
    }

    public class ParcelaModel
    {
        [Key]
        public int Id { get; set; }
        public int NumeroParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorParcela { get; set; }
    }
}