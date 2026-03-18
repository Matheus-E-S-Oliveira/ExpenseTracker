using ExpenseTracker.WebApi.Structure.Binding.Actions;
using ExpenseTracker.WebApi.Structure.Binding.Providers.Interfaces;

namespace ExpenseTracker.WebApi.Structure.Binding.Providers.Implementations
{
    /// <summary>
    /// Provedor de valores responsável por obter dados a partir da query string da requisição HTTP.
    ///
    /// Utiliza os parâmetros presentes na URL após o '?',
    /// como por exemplo: /recurso?pagina=1.
    /// </summary>
    public class QueryValueProvider : IBindingValueProvider
    {
        /// <summary>
        /// Obtém o valor da query string com base no nome definido na ação de binding.
        /// </summary>
        /// <param name="context">Contexto HTTP da requisição</param>
        /// <param name="action">Configuração da ação de binding</param>
        /// <returns>Valor da query ou null caso não encontrado ou vazio</returns>
        public object? GetValue(HttpContext context, BinderAction action)
        {
            var value = context.Request.Query[action.Name];

            // Retorna null caso o valor esteja vazio ou não exista
            return string.IsNullOrWhiteSpace(value) ? null : value.ToString();
        }
    }
}
