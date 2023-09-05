using Teste.BackEnd.Application.AppServices.Interfaces;
using Teste.BackEnd.Application.Dto;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Application.AppServices
{
    public class CreditoPessoaJuridicaAppService : ICreditoPessoaJuridicaAppService
    {
        public Result<CreditResponseDTO> ProcessaLiberacaoCredito(CreditRequestDTO creditRequest)
        {
            if (!ValidacoesCreditoAppService.ValidarCessaoCredito(creditRequest) || !ValidaPisoLiberacaoCredito(creditRequest))
            {
                var response = new CreditResponseDTO();

                response.Status = "Reprovado";
                response.ValorTotalComJuros = 0;
                response.ValorJuros = 0;

                return response;
            }

            return EfetuaCalculoCreditoPessoaJuridica(creditRequest);
        }

        private bool ValidaPisoLiberacaoCredito(CreditRequestDTO creditRequest)
            => creditRequest.ValorCredito >= 15000;

        private Result<CreditResponseDTO> EfetuaCalculoCreditoPessoaJuridica(CreditRequestDTO creditRequest)
        {
            var response = new CreditResponseDTO();

            // Cálculos dos juros
            double taxa = 0.05;  // Taxa de 5% para crédito Pessoa Juridica
            double valorJuros = creditRequest.ValorCredito * taxa;
            double valorTotalComJuros = creditRequest.ValorCredito + valorJuros;

      
            response.Status = "Aprovado";
            response.ValorTotalComJuros = valorTotalComJuros;
            response.ValorJuros = valorJuros;

            return response;
        }
    }
}
