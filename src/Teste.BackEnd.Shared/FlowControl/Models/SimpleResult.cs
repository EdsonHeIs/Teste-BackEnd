using System.Collections.ObjectModel;

namespace Teste.BackEnd.Shared.FlowControl.Models
{
    public class SimpleResult : ResultBase
    {
        public SimpleResult(IEnumerable<Error> erros) : base(erros)
        { }

        public static SimpleResult Success() => new(Array.Empty<Error>());
        public static SimpleResult Fail(IEnumerable<Error> errors) => new(errors);
        public static SimpleResult Fail(Error error) => new(new Error[] { error });

        public static implicit operator SimpleResult(Error error) => Fail(error);
        public static implicit operator SimpleResult(ReadOnlyCollection<Error> errors) => Fail(errors);
        public static implicit operator SimpleResult(List<Error> errors) => Fail(errors);
        public static implicit operator SimpleResult(Error[] errors) => Fail(errors);
    }
}
