using Microsoft.Extensions.DependencyInjection;
using Teste.BackEnd.Application.AppServices;
using Teste.BackEnd.Application.AppServices.Interfaces;

namespace Teste.BackEnd.Application.IoC
{
    public static class ApplicationIoC
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services) =>
            services.AddScoped<ICreditoConsignadoAppService, CreditoConsignadoAppService>()
                    .AddScoped<ICreditoDiretoAppService, CreditoDiretoAppService>()
                    .AddScoped<ICreditoImobiliarioAppService, CreditoImobiliarioAppService>()
                    .AddScoped<ICreditoPessoaFisicaAppService, CreditoPessoaFisicaAppService>()
                    .AddScoped<ICreditoPessoaJuridicaAppService, CreditoPessoaJuridicaAppService>();

    }
}
