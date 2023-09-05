

using Teste.BackEnd.Application.Dto;

namespace Teste.BackEnd.Application.AppServices
{
    internal static class ValidacoesCreditoAppService
    {
        public static bool ValidarCessaoCredito(CreditRequestDTO request)
        {
            return (ValidarValor(request) && ValidarParcelas(request) && ValidarDataVencimento(request));
               
        }

        private static bool ValidarValor(CreditRequestDTO request) => 
            request.ValorCredito <= 1000000;

        private static bool ValidarParcelas(CreditRequestDTO request) => 
            request.Parcelas >= 5 && request.Parcelas <= 72;
   
        private static bool ValidarDataVencimento(CreditRequestDTO request)
        {
            DateTime dataAtual = DateTime.Now;
            DateTime minVencimento = dataAtual.AddDays(15);
            DateTime maxVencimento = dataAtual.AddDays(40);
            return request.PrimeiroVencimento >= minVencimento && request.PrimeiroVencimento <= maxVencimento;
        }
    }
}
