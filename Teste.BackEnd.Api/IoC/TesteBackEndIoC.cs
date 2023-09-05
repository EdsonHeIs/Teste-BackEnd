using Teste.BackEnd.Api.Mapper;
using Teste.BackEnd.Api.Strategies;
using Teste.BackEnd.Api.Strategies.Interfaces;

namespace Teste.BackEnd.Api.IoC
{
    public static class TesteBackEndIoC
    {
        public static IServiceCollection AddStrategies(this IServiceCollection services)
        {
            services.AddSingleton<IStrategyExecution, StrategyExecution>();
            services.AddScoped<ICreditoConsignadoStrategy, CreditoConsignadoStrategy>();
            services.AddScoped<ICreditoDiretoStrategy, CreditoDiretoStrategy>();
            services.AddScoped<ICreditoPessoaFisicaStrategy, CreditoPessoaFisicaStrategy>();
            services.AddScoped<ICreditoPessoaJuridicaStrategy, CreditoPessoaJuridicaStrategy>();
            services.AddScoped<ICreditoImobiliarioStrategy, CreditoImobiliarioStrategy>();

            return services;
        }

        public static IServiceCollection AddApiMapping(this IServiceCollection services) =>
           services.AddAutoMapper(typeof(MappingProfile));
    }
}
