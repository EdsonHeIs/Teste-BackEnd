namespace Teste.BackEnd.Application.Dto
{
    public class CreditRequestDTO
    {
        public double ValorCredito { get; set; }
        public string? TipoCredito { get; set; }
        public int Parcelas { get; set; }
        public DateTime PrimeiroVencimento { get; set; }
    }
}
