namespace Teste.BackEnd.Api.Models
{
    public class CreditRequest
    {
        public double ValorCredito { get; set; }
        public string? TipoCredito { get; set; }
        public int Parcelas { get; set; }
        public DateTime PrimeiroVencimento { get; set; }
    }
}
