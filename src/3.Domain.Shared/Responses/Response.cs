using System.Text.Json.Serialization;
using Domain.Shared.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Domain.Shared.Responses
{
    public class Response
    {
        private List<string> _errors = new();

        [SwaggerSchema(Description = "Id da entidade raiz de agregação", Nullable = true, ReadOnly = true)]
        public Guid? Id { get; set; }

        [SwaggerSchema(Description = "Mensagem interna do sistema", Nullable = true, ReadOnly = true)]
        public string? InternalMessage { get; set; }

        [SwaggerSchema(Description = "Lista de erros a amigáveis para o usuário", Nullable = true, ReadOnly = true)]
        public IReadOnlyList<string>? Errors => _errors.AsReadOnly();

        [JsonIgnore] public int? StatusCode { get; set; }

        public void AddError(EErrorCode errorCode, params object[] args)
        {
            AddError(errorCode.ToString(), args);
        }

        public void AddErrors(IEnumerable<EErrorCode> errorCodes)
        {
            foreach (var errorCode in errorCodes)
            {
                AddError(errorCode);
            }
        }

        public void AddError(string error, params object[] args)
        {
            _errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            _errors.AddRange(errors);
        }
    }
}