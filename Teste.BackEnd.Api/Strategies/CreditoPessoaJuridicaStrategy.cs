using AutoMapper;
using Teste.BackEnd.Api.Models;
using Teste.BackEnd.Api.Strategies.Interfaces;
using Teste.BackEnd.Application.AppServices.Interfaces;
using Teste.BackEnd.Application.Dto;
using Teste.BackEnd.Shared.FlowControl.Enums;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Api.Strategies
{
    public class CreditoPessoaJuridicaStrategy : ICreditoPessoaJuridicaStrategy
    {
        private readonly ICreditoPessoaJuridicaAppService _creditoPessoaJuridicaAppService;
        private readonly IMapper _mapper;
        public CreditoPessoaJuridicaStrategy(IMapper mapper, ICreditoPessoaJuridicaAppService creditoPessoaJuridicaAppService)
        {
            _mapper = mapper;
            _creditoPessoaJuridicaAppService = creditoPessoaJuridicaAppService;
        }

        public Result<CreditResponse> Calcular(CreditRequest creditRequest)
        {
            if (creditRequest == null)
                return new Error(ErrorType.Business, "Calculo Credito PessoaJuridica", "Dados insuficientes para efeutar o calculo");

            var requestDTO = _mapper.Map<CreditRequestDTO>(creditRequest);

            var responseDTO = _creditoPessoaJuridicaAppService.ProcessaLiberacaoCredito(requestDTO);

            var response = _mapper.Map<CreditResponse>(responseDTO.Value);

            return response;
        }
    }
}
