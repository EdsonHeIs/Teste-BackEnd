using System;
using Teste.BackEnd.Application.AppServices;
using Teste.BackEnd.Application.Dto;
using Xunit;

namespace Teste.BackEnd.Tests.Application.Tests
{
    public class CreditoImobiliarioAppServiceTests
    {
        [Fact]
        public void ProcessaLiberacaoCredito_ValidRequest_ReturnsApprovedResponse()
        {
            // Arrange
            var creditRequest = new CreditRequestDTO
            {
                ValorCredito = 1000, // Valor válido
                TipoCredito = "CREDITO_IMOBILIARIO",
                Parcelas = 12,
                PrimeiroVencimento = DateTime.Now.AddDays(21),
            };

            var appService = new CreditoImobiliarioAppService();

            // Act
            var result = appService.ProcessaLiberacaoCredito(creditRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Aprovado", result.Value.Status);
            Assert.NotEqual(0, result.Value.ValorTotalComJuros);
            Assert.NotEqual(0, result.Value.ValorJuros);
        }

        [Fact]
        public void ProcessaLiberacaoCredito_InvalidRequest_ReturnsRejectedResponse()
        {
            // Arrange
            var creditRequest = new CreditRequestDTO
            {
                ValorCredito = 10000,
                TipoCredito = "CREDITO_IMOBILIARIO",
                Parcelas = 12,
                PrimeiroVencimento = DateTime.Now
            };

            var appService = new CreditoImobiliarioAppService();

            // Act
            var result = appService.ProcessaLiberacaoCredito(creditRequest);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal("Reprovado", result.Value.Status);
            Assert.Equal(0, result.Value.ValorTotalComJuros);
            Assert.Equal(0, result.Value.ValorJuros);
        }
        [Fact]
        public void ProcessaLiberacaoCredito_ValidRequest_ReturnsApprovedResponseWithCorrectInterestRate()
        {
            // Arrange
            var creditRequest = new CreditRequestDTO
            {
                ValorCredito = 1000,
                TipoCredito = "CREDITO_IMOBILIARIO",
                Parcelas = 12,
                PrimeiroVencimento = DateTime.Now.AddDays(21),
            };

            var appService = new CreditoImobiliarioAppService();

            // Define a taxa de juros esperada para a entrada
            double expectedInterestRate = 0.09; // 9%

            // Act
            var result = appService.ProcessaLiberacaoCredito(creditRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Aprovado", result.Value.Status);
            Assert.Equal(expectedInterestRate, result.Value.ValorJuros / creditRequest.ValorCredito, 2); // Verifique a taxa com uma precisão de 2 casas decimais
        }
    }
}
