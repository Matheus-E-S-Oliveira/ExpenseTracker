using ExpenseTracker.WebApi.Structure.Binding.Actions;
using ExpenseTracker.WebApi.Structure.Binding.Providers.Interfaces;

namespace ExpenseTracker.WebApi.Structure.Binding.Providers.Implementations
{
    /// <summary>
    /// Provedor de valores responsável por obter dados a partir da rota da requisição HTTP.
    ///
    /// Utiliza os valores definidos na URL (RouteValues), normalmente associados
    /// a parâmetros de rota como /recurso/{id}.
    /// </summary>
    public class RouteValueProvider : IBindingValueProvider
    {
        /// <summary>
        /// Obtém o valor da rota com base no nome definido na ação de binding.
        /// </summary>
        /// <param name="context">Contexto HTTP da requisição</param>
        /// <param name="action">Configuração da ação de binding</param>
        /// <returns>Valor da rota ou null caso não encontrado</returns>
        public object? GetValue(HttpContext context, BinderAction action)
        {
            return context.Request.RouteValues.TryGetValue(action.Name, out var value) ? value : null;
        }
    }
}
