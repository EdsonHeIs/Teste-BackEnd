using System.Collections.ObjectModel;
using System.Net;
using Teste.BackEnd.Shared.FlowControl.Models;

namespace Teste.BackEnd.Shared.FlowControl.Interfaces
{
    public interface IResultBase
    {
        bool HasError { get; }
        ReadOnlyCollection<Error> Errors { get; }
        HttpStatusCode ConvertToHttpStatusCode();
        string ErrorsToString();
    }
}
