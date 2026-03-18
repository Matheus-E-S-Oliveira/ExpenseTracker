using ExpenseTracker.WebApi.Structure.Common.Responses;

namespace ExpenseTracker.WebApi.Structure.Common.Results.Core
{
    /// <summary>
    /// Representa um resultado de sucesso para operações da API.
    /// 
    /// Encapsula os dados retornados, mensagem opcional e código HTTP,
    /// convertendo para um formato padronizado de resposta.
    /// </summary>
    /// <typeparam name="T">Tipo dos dados retornados</typeparam>
    public sealed class SuccessResult<T>(T data, int statusCode, string? message = null) : EndpointResult(statusCode)
    {
        /// <summary>
        /// Dados retornados pela operação.
        /// </summary>
        public T Data { get; } = data;

        /// <summary>
        /// Mensagem opcional da resposta.
        /// </summary>
        public string? Message { get; } = message;

        /// <summary>
        /// Converte o resultado para um IResult (HTTP).
        /// </summary>
        /// <returns>Resposta HTTP padronizada</returns>
        public override IResult ToHttpResult()
        {
            var response = new ApiResponse<T>
            {
                Success = true,
                StatusCode = StatusCode,
                Message = Message,
                Data = Data
            };

            return StatusCode switch
            {
                200 => TypedResults.Ok(response),
                201 => TypedResults.Created("", response),
                _   => TypedResults.Json(response, statusCode: StatusCode)
            };
        }
    }
}
