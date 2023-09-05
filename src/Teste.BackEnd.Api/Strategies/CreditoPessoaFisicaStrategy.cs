using AutoMapper;
using Teste.BackEnd.Api.Models;
using Teste.BackEnd.Api.Strategies.Interfaces;
using Teste.BackEnd.Application.AppServices.Interfaces;
using Teste.BackEnd.Application.Dto;
using Teste.BackEnd.Shared.FlowControl.Enums;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Api.Strategies
{
    public class CreditoPessoaFisicaStrategy : ICreditoPessoaFisicaStrategy
    {
        private readonly ICreditoPessoaFisicaAppService _creditoPessoaFisicaAppService;
        private readonly IMapper _mapper;
        public CreditoPessoaFisicaStrategy(IMapper mapper, ICreditoPessoaFisicaAppService creditoPessoaFisicaAppService)
        {
            _mapper = mapper;
            _creditoPessoaFisicaAppService = creditoPessoaFisicaAppService;
        }

        public Result<CreditResponse> Calcular(CreditRequest creditRequest)
        {
            if (creditRequest == null)
                return new Error(ErrorType.Business, "Calculo Credito PessoaFisica", "Dados insuficientes para efeutar o calculo");

            var requestDTO = _mapper.Map<CreditRequestDTO>(creditRequest);

            var responseDTO = _creditoPessoaFisicaAppService.ProcessaLiberacaoCredito(requestDTO);

            var response = _mapper.Map<CreditResponse>(responseDTO.Value);

            return response;
        }
    }
}
