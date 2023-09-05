using System.Collections.ObjectModel;
using System.Net;
using Teste.BackEnd.Shared.FlowControl.Enums;
using Teste.BackEnd.Shared.FlowControl.Interfaces;

namespace Teste.BackEnd.Shared.FlowControl.Models
{
    public class ResultBase : IResultBase
    {
        public bool HasError => Errors.Any();
        public ReadOnlyCollection<Error> Errors { get; }

        public ResultBase(IEnumerable<Error> erros)
        {
            Errors = new ReadOnlyCollection<Error>(erros.ToArray());
        }

        public HttpStatusCode ConvertToHttpStatusCode()
        {
            if (!Errors.Any())
                return HttpStatusCode.OK;

            var firstErrorType = Errors.First().Type;

            return firstErrorType switch
            {
                ErrorType.Business => HttpStatusCode.BadRequest,
                ErrorType.Unhandled => HttpStatusCode.InternalServerError,
                ErrorType.FailedDependency => HttpStatusCode.FailedDependency,
                _ => HttpStatusCode.InternalServerError
            };
        }

        public string ErrorsToString() => string.Join("; ", Errors);
    }
}
