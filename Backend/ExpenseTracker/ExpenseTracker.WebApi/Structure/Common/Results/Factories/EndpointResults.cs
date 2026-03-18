using ExpenseTracker.WebApi.Structure.Common.Results.Core;

namespace ExpenseTracker.WebApi.Structure.Common.Results.Factories
{
    /// <summary>
    /// Factory responsável por criar instâncias padronizadas de resultados de endpoints.
    /// 
    /// Facilita a criação de respostas consistentes e reduz repetição de código.
    /// </summary>
    public static class EndpointResults
    {
        /// <summary>
        /// Retorna um resultado de sucesso (200 - OK).
        /// </summary>
        public static SuccessResult<T> Ok<T>(T data, string? message = null) => new(data, 200, message);

        /// <summary>
        /// Retorna um resultado de criação (201 - Created).
        /// </summary>
        public static SuccessResult<T> Created<T>(T data, string? message = null) => new(data, 201, message);

        /// <summary>
        /// Retorna um erro de requisição inválida (400 - BadRequest).
        /// </summary>
        public static ErrorResult BadRequest(string message) => new(message, 400);

        /// <summary>
        /// Retorna um erro de recurso não encontrado (404 - NotFound).
        /// </summary>
        public static ErrorResult NotFound(string message) => new(message, 404);

        /// <summary>
        /// Retorna um erro de não autorizado (401 - Unauthorized).
        /// </summary>
        public static ErrorResult Unauthorized() => new("Unauthorized", 401);

        /// <summary>
        /// Retorna um erro de validação (400 - BadRequest) com detalhes.
        /// </summary>
        public static ErrorResult Validation(object errors) => new("Validation failed", 400, errors);
    }
}
