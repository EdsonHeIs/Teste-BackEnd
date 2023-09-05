using System.Collections.ObjectModel;
using Teste.BackEnd.Shared.FlowControl.Interfaces;

namespace Teste.BackEnd.Shared.FlowControl.Models
{
    public class Result<T> : ResultBase, IResult<T>
    {
        public T Value => GetSuccessValue();

        private readonly T? _value;

        public Result(IEnumerable<Error> erros, T? value) : base(erros)
        {
            _value = value;
        }

        public static Result<T> Success(T data) =>
             new(Array.Empty<Error>(), data);

        public static Result<T> Fail(IEnumerable<Error> errors) =>
             new(errors, default);

        public static Result<T> Fail(Error error) =>
             new(new List<Error> { error }, default);

        private T GetSuccessValue()
        {
            if (_value == null)
                throw new InvalidOperationException($"Result is in status failed. Value is not set. Having: {ErrorsToString()}");

            return _value;
        }

        public static implicit operator Result<T>(Error error) => Fail(error);
        public static implicit operator Result<T>(ReadOnlyCollection<Error> errors) => Fail(errors);
        public static implicit operator Result<T>(List<Error> errors) => Fail(errors);
        public static implicit operator Result<T>(Error[] errors) => Fail(errors);
        public static implicit operator Result<T>(T value) => Success(value);

        public static implicit operator SimpleResult(Result<T> result)
        {
            if (result.HasError)
                return SimpleResult.Fail(result.Errors);

            return SimpleResult.Success();
        }
    }
}
