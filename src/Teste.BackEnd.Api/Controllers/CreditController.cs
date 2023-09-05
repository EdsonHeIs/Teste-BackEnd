using Microsoft.AspNetCore.Mvc;
using Teste.BackEnd.Api.Models;
using Teste.BackEnd.Api.Strategies.Interfaces;

namespace Teste.BackEnd.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditController : ControllerBase
    {
        private readonly ILogger<CreditController> _logger;
        private readonly IStrategyExecution _strategyExecution;

        public CreditController(ILogger<CreditController> logger, IStrategyExecution strategyExecution)
        {
            _logger = logger;
            _strategyExecution = strategyExecution;

        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<CreditResponse> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new CreditResponse
            {
                Status = "TESTE",
                ValorJuros = Random.Shared.Next(-20, 55),
                ValorTotalComJuros = 2000.5
            })
            .ToArray();
        }

        [HttpPost]
        [Route("processarCredito")]
        public async Task<IActionResult> ProcessarCreditoAsync([FromBody] CreditRequest request)
        {
            _logger.LogInformation("Liberação Credito {@TipoCredito}", request.TipoCredito);

            var executionResult = _strategyExecution.Calcular(request);

            if (executionResult.HasError)
            {
                _logger.LogError("Erro ao processar requisição: {@Errors}", executionResult.Errors);
                return BadRequest(new { Error = "Erro ao processar a solicitação", Details = executionResult.Errors });
            }

            await Task.Delay(1000);

            return Ok(new { Success = "Requisição de Credito Executada com sucesso", request.TipoCredito, Data = executionResult });
        }


    }
}