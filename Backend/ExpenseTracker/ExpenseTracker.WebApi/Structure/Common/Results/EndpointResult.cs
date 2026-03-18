namespace ExpenseTracker.WebApi.Structure.Common.Results
{
    /// <summary>
    /// Representa um resultado base para endpoints da aplicação.
    /// 
    /// Abstrai a criação de respostas HTTP, permitindo padronização
    /// e desacoplamento da implementação direta do ASP.NET (IResult).
    /// </summary>
    public abstract class EndpointResult(int statusCode)
    {
        /// <summary>
        /// Código de status HTTP da resposta.
        /// </summary>
        public int StatusCode { get; } = statusCode;

        /// <summary>
        /// Converte o resultado da aplicação para um resultado HTTP (IResult).
        /// </summary>
        /// <returns>Resultado HTTP correspondente</returns>
        public abstract IResult ToHttpResult();
    }
}
