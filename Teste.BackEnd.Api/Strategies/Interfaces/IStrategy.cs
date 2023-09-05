using Teste.BackEnd.Api.Models;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Api.Strategies.Interfaces
{
    public interface IStrategy
    {
        Result<CreditResponse> Calcular(CreditRequest creditRequest);
    }
}
