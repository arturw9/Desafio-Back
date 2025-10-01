using Desafio.ViewModels;

namespace Desafio.Helpers
{
    public class TituloHelper
    {
        public string VerificarCampos(TituloViewModel vm)
        {
            if (vm == null) return "Não foram enviados dados.";

            // Validação de strings
            if (string.IsNullOrWhiteSpace(vm.NomeDevedor)) return "Campo NomeDevedor é obrigatório.";
            if (string.IsNullOrWhiteSpace(vm.NumeroTitulo)) return "Campo NumeroTitulo é obrigatório.";
            if (string.IsNullOrWhiteSpace(vm.CpfDevedor)) return "Campo CpfDevedor é obrigatório.";

            // Validação de números
            if (vm.PercentualJuros <= 0) return "Campo PercentualJuros deve ser maior que zero.";
            if (vm.PercentualMulta <= 0) return "Campo PercentualMulta deve ser maior que zero.";

            // Validação de parcelas
            if (vm.Parcelas == null || !vm.Parcelas.Any()) return "Nenhuma parcela informada.";
            if (vm.Parcelas.Any(p => p.DataVencimento > DateTime.Now || p.ValorParcela <= 0 || p.NumeroParcela <= 0))
                return "Parcelas com data, valor ou número inválidos.";

            return ""; // todos os campos válidos
        }
    }
}