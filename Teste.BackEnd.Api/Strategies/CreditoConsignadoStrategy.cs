﻿using AutoMapper;
using Teste.BackEnd.Api.Models;
using Teste.BackEnd.Api.Strategies.Interfaces;
using Teste.BackEnd.Application.AppServices.Interfaces;
using Teste.BackEnd.Application.Dto;
using Teste.BackEnd.Shared.FlowControl.Enums;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Api.Strategies
{
    public class CreditoConsignadoStrategy : ICreditoConsignadoStrategy
    {
        private readonly ICreditoConsignadoAppService _creditoConsignadoAppService;
        private readonly IMapper _mapper;
        public CreditoConsignadoStrategy(IMapper mapper, ICreditoConsignadoAppService creditoConsignadoAppService)
        {
            _mapper = mapper;   
            _creditoConsignadoAppService = creditoConsignadoAppService;
        }

        public Result<CreditResponse> Calcular(CreditRequest creditRequest)
        {
            if (creditRequest == null)
                return new Error(ErrorType.Business, "Calculo Credito Consignado", "Dados insuficientes para efeutar o calculo");

            var requestDTO = _mapper.Map<CreditRequestDTO>(creditRequest);

            var responseDTO =  _creditoConsignadoAppService.ProcessaLiberacaoCredito(requestDTO);

            var response = _mapper.Map<CreditResponse>(responseDTO.Value);

            return response;
        }
    }
}
