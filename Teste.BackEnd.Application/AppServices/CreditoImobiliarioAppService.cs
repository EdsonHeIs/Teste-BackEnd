using Teste.BackEnd.Application.AppServices.Interfaces;
using Teste.BackEnd.Application.Dto;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Application.AppServices
{
    public class CreditoImobiliarioAppService : ICreditoImobiliarioAppService
    {
        public Result<CreditResponseDTO> ProcessaLiberacaoCredito(CreditRequestDTO creditRequest)
        {
            if (!ValidacoesCreditoAppService.ValidarCessaoCredito(creditRequest))
            {
                var response = new CreditResponseDTO();

                response.Status = "Reprovado";
                response.ValorTotalComJuros = 0;
                response.ValorJuros = 0;

                return response;
            }

            return EfetuaCalculoCreditoImobiliario(creditRequest);
        }

        private Result<CreditResponseDTO> EfetuaCalculoCreditoImobiliario(CreditRequestDTO creditRequest)
        {
            var response = new CreditResponseDTO();

            // Cálculos dos juros
            double taxa = 0.09;  // Taxa de 9% para crédito imobiliario
            double valorJuros = creditRequest.ValorCredito * taxa;
            double valorTotalComJuros = creditRequest.ValorCredito + valorJuros;

            // Preencher a resposta
            response.Status = "Aprovado";
            response.ValorTotalComJuros = valorTotalComJuros;
            response.ValorJuros = valorJuros;

            return response;
        }
    }
}
