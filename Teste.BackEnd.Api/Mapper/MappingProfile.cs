using AutoMapper;
using Teste.BackEnd.Api.Models;
using Teste.BackEnd.Application.Dto;

namespace Teste.BackEnd.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreditRequest, CreditRequestDTO>();
            CreateMap<CreditResponseDTO, CreditResponse>();
        }
    }
}
