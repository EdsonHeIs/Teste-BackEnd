using AutoMapper;
using Teste.BackEnd.Api.Models;
using Teste.BackEnd.Api.Strategies.Interfaces;
using Teste.BackEnd.Application.AppServices.Interfaces;
using Teste.BackEnd.Application.Dto;
using Teste.BackEnd.Shared.FlowControl.Enums;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Api.Strategies
{
    public class CreditoDiretoStrategy : ICreditoDiretoStrategy
    {
        private readonly ICreditoDiretoAppService _creditoDiretoAppService;
        private readonly IMapper _mapper;
        public CreditoDiretoStrategy(IMapper mapper, ICreditoDiretoAppService creditoDiretoAppService)
        {
            _mapper = mapper;
            _creditoDiretoAppService = creditoDiretoAppService;
        }

        public Result<CreditResponse> Calcular(CreditRequest creditRequest)
        {
            if (creditRequest == null)
                return new Error(ErrorType.Business, "Calculo Credito Direto", "Dados insuficientes para efeutar o calculo");

            var requestDTO = _mapper.Map<CreditRequestDTO>(creditRequest);

            var responseDTO = _creditoDiretoAppService.ProcessaLiberacaoCredito(requestDTO);

            var response = _mapper.Map<CreditResponse>(responseDTO.Value);

            return response;
        }
    }
}
