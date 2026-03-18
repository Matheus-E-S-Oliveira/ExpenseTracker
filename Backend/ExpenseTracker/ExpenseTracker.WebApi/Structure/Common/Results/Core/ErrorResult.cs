
using ExpenseTracker.WebApi.Structure.Common.Responses;

namespace ExpenseTracker.WebApi.Structure.Common.Results.Core
{
    /// <summary>
    /// Representa um resultado de erro para operações da API.
    /// 
    /// Encapsula mensagem, código HTTP e detalhes adicionais de erro,
    /// convertendo para um formato padronizado de resposta.
    /// </summary>
    public class ErrorResult(string message, int statusCode, object? errors = null) : EndpointResult(statusCode)
    {
        /// <summary>
        /// Mensagem principal do erro.
        /// </summary>
        public string Message { get; } = message;

        /// <summary>
        /// Detalhes adicionais do erro (ex: validações).
        /// </summary>
        public object? Errors { get; } = errors;

        /// <summary>
        /// Converte o resultado para um IResult (HTTP).
        /// </summary>
        public override IResult ToHttpResult()
        {
            var response = new ApiResponse<object>
            {
                Success = false,
                StatusCode = StatusCode,
                Message = Message,
                Errors = Errors
            };

            return StatusCode switch
            {
                400 => TypedResults.BadRequest(response),
                404 => TypedResults.NotFound(response),
                401 => TypedResults.Unauthorized(), 
                _  => TypedResults.Json(response, statusCode: StatusCode)
            };
        }
    }
}
