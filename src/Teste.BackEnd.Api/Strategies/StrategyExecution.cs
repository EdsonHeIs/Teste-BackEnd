using Serilog.Context;
using Teste.BackEnd.Api.Models;
using Teste.BackEnd.Api.Strategies.Interfaces;
using Teste.BackEnd.Shared.FlowControl.Enums;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Api.Strategies
{
    public class StrategyExecution : IStrategyExecution
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        private readonly Dictionary<string, Func<IServiceScope, IStrategy>> strategies =
           new()
           {
               { "CREDITO_DIRETO", scope => scope.ServiceProvider.GetRequiredService<ICreditoDiretoStrategy>() },
               { "CREDITO_CONSIGNADO", scope => scope.ServiceProvider.GetRequiredService<ICreditoConsignadoStrategy>() },
               { "CREDITO_PESSOA_JURIDICA", scope => scope.ServiceProvider.GetRequiredService<ICreditoPessoaJuridicaStrategy>() },
               { "CREDITO_PESSOA_FISICA", scope => scope.ServiceProvider.GetRequiredService<ICreditoPessoaFisicaStrategy>() },
               { "CREDITO_IMOBILIARIO", scope => scope.ServiceProvider.GetRequiredService<ICreditoImobiliarioStrategy>() }
           };

        public StrategyExecution(ILogger<StrategyExecution> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Result<CreditResponse> Calcular(CreditRequest creditRequest)
        {
            if (creditRequest.TipoCredito == null)
            {
                _logger.LogError("Não foi possével obter o tipo de crédito solicitado '{TipoCredito}'", creditRequest.TipoCredito);
                return new Error(ErrorType.Business, "KeyNotFound", "Não foi possível obter o valor do atributo TipoCredito da requisição.");
            }

            if (!strategies.TryGetValue(creditRequest.TipoCredito, out var getStrategy))
            {
                _logger.LogError("Não foi possível processar a requisição, pois não existe estratégia para  {TipoCredito}.", creditRequest.TipoCredito);
                return new Error(ErrorType.Business, "UnknownRequestType", $"Não foi possível processar a requisição, pois o tipo de crédito é desconhecido");
            }

            using (LogContext.PushProperty("TipoCredito", creditRequest.TipoCredito))
            {
                using var scope = _serviceProvider.CreateScope();
                var strategy = getStrategy(scope);

                return strategy.Calcular(creditRequest);
            }
        }
    }
}
