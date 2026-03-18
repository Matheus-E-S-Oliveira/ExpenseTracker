using System.Text.Json.Serialization;

namespace ExpenseTracker.WebApi.Structure.Common.Responses
{
    /// <summary>
    /// Representa o padrão de resposta da API.
    /// 
    /// Encapsula informações sobre o sucesso da operação,
    /// código HTTP, mensagem, dados retornados e possíveis erros.
    /// </summary>
    /// <typeparam name="T">Tipo dos dados retornados</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indica se a operação foi bem-sucedida.
        /// </summary>
        public bool Success { get; init; }

        /// <summary>
        /// Código de status HTTP da resposta.
        /// </summary>
        public int StatusCode { get; init; }

        /// <summary>
        /// Mensagem descritiva da resposta (opcional).
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; init; }

        /// <summary>
        /// Dados retornados pela operação (opcional).
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; init; }

        /// <summary>
        /// Informações de erro da operação (opcional).
        /// Pode conter detalhes de validação ou exceções.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Errors { get; init; }
    }
}

