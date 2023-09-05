using System;
using Teste.BackEnd.Application.AppServices;
using Teste.BackEnd.Application.Dto;
using Xunit;

namespace Teste.BackEnd.Tests.Application.Tests
{
    public class CreditoPessoaJuridicaAppServiceTests
    {
        [Fact]
        public void ProcessaLiberacaoCredito_ValidRequest_ReturnsApprovedResponse()
        {
            // Arrange
            var creditRequest = new CreditRequestDTO
            {
                ValorCredito = 15000, // Valor válido
                TipoCredito = "CREDITO_PESSOA_JURIDICA",
                Parcelas = 12,
                PrimeiroVencimento = DateTime.Now.AddDays(21),
            };

            var appService = new CreditoPessoaJuridicaAppService();

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
                TipoCredito = "CREDITO_PESSOA_JURIDICA",
                Parcelas = 12,
                PrimeiroVencimento = DateTime.Now
            };

            var appService = new CreditoPessoaJuridicaAppService();

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
                ValorCredito = 15000,
                TipoCredito = "CREDITO_PESSOA_JURIDICA",
                Parcelas = 12,
                PrimeiroVencimento = DateTime.Now.AddDays(21),
            };

            var appService = new CreditoPessoaJuridicaAppService();

            // Define a taxa de juros esperada para a entrada
            double expectedInterestRate = 0.05; // 5%

            // Act
            var result = appService.ProcessaLiberacaoCredito(creditRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Aprovado", result.Value.Status);
            Assert.Equal(expectedInterestRate, result.Value.ValorJuros / creditRequest.ValorCredito, 2); // Verifique a taxa com uma precisão de 2 casas decimais
        }

        [Theory]
        [InlineData(14999, "Reprovado")] // Valor abaixo do piso
        [InlineData(15000, "Aprovado")]  // Valor igual ao piso
        [InlineData(15001, "Aprovado")]  // Valor acima do piso
        public void ValidaPisoLiberacaoCredito_ValidatesCorrectly(decimal valorCredito, string expectedResult)
        {
            // Arrange
            var creditRequest = new CreditRequestDTO
            {
                ValorCredito = (double)valorCredito,
                Parcelas= 12,
                PrimeiroVencimento= DateTime.Now.AddDays(21), 
                TipoCredito = "CREDITO_PESSOA_JURIDICA"

            };

            var appService = new CreditoPessoaJuridicaAppService();

            // Act
            var result = appService.ProcessaLiberacaoCredito(creditRequest);

            // Assert
            Assert.Equal(expectedResult, result.Value.Status);
        }
    }
}
