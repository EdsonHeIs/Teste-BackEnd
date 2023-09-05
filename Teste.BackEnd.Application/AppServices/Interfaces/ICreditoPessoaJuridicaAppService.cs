using Teste.BackEnd.Application.Dto;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Application.AppServices.Interfaces
{
    public interface ICreditoPessoaJuridicaAppService
    {
        Result<CreditResponseDTO> ProcessaLiberacaoCredito(CreditRequestDTO creditRequest);
    }
}
