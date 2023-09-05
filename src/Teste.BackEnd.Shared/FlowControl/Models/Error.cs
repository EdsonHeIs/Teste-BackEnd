using Newtonsoft.Json;
using Teste.BackEnd.Shared.FlowControl.Enums;

namespace Teste.BackEnd.Shared.FlowControl.Models
{
    public class Error
    {
        [JsonProperty(Required = Required.Always)]
        public ErrorType Type { get; }

        [JsonProperty(Required = Required.Always)]
        public string Code { get; }

        [JsonProperty(Required = Required.Always)]
        public string Message { get; }

        [JsonConstructor]
        [System.Text.Json.Serialization.JsonConstructor]
        public Error(ErrorType type, string code, string message)
        {
            Type = type;
            Code = code;
            Message = message;
        }

        public override string ToString()
        {
            return $"[{Type}][{Code}] {Message}";
        }
    }
}
