using Teste.BackEnd.Application.AppServices.Interfaces;
using Teste.BackEnd.Application.Dto;
using Teste.BackEnd.Shared.FlowControl.Enums;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Application.AppServices
{
    public class CreditoConsignadoAppService : ICreditoConsignadoAppService
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

            return EfetuaCalculoCosignado(creditRequest);
        }

        private Result<CreditResponseDTO> EfetuaCalculoCosignado(CreditRequestDTO creditRequest)
        {
            var response = new CreditResponseDTO();

            // Cálculos dos juros
            double taxa = 0.01;  // Taxa de 1% para crédito consignado
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
