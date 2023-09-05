namespace Teste.BackEnd.Shared.FlowControl.Interfaces
{
    public interface IResult<out T> : IResultBase
    {
        public T Value { get; }
    }
}
