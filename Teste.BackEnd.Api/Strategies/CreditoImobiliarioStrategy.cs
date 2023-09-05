using AutoMapper;
using Teste.BackEnd.Api.Models;
using Teste.BackEnd.Api.Strategies.Interfaces;
using Teste.BackEnd.Application.AppServices.Interfaces;
using Teste.BackEnd.Application.Dto;
using Teste.BackEnd.Shared.FlowControl.Enums;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Api.Strategies
{
    public class CreditoImobiliarioStrategy : ICreditoImobiliarioStrategy
    {
       
        private readonly ICreditoImobiliarioAppService _creditoImobiliarioAppService;
        private readonly IMapper _mapper;
        public CreditoImobiliarioStrategy(IMapper mapper, ICreditoImobiliarioAppService creditoImobiliarioAppService)
        {
            _mapper = mapper;
            _creditoImobiliarioAppService = creditoImobiliarioAppService;
        }

        public Result<CreditResponse> Calcular(CreditRequest creditRequest)
        {
            if (creditRequest == null)
                return new Error(ErrorType.Business, "Calculo Credito Imobiliario", "Dados insuficientes para efeutar o calculo");

            var requestDTO = _mapper.Map<CreditRequestDTO>(creditRequest);

            var responseDTO = _creditoImobiliarioAppService.ProcessaLiberacaoCredito(requestDTO);

            var response = _mapper.Map<CreditResponse>(responseDTO.Value);

            return response;
        }
    }
}
