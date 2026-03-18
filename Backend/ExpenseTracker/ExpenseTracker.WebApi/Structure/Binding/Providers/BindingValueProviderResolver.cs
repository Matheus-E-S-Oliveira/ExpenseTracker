using ExpenseTracker.WebApi.Structure.Binding.Actions;
using ExpenseTracker.WebApi.Structure.Binding.Core;
using ExpenseTracker.WebApi.Structure.Binding.Providers.Implementations;
using ExpenseTracker.WebApi.Structure.Binding.Providers.Interfaces;

namespace ExpenseTracker.WebApi.Structure.Binding.Providers
{
    /// <summary>
    /// Responsável por resolver o provedor de valores com base na origem
    /// definida no processo de binding (BindingSource).
    ///
    /// Atua como um ponto central de decisão para obter valores da requisição,
    /// delegando a responsabilidade para o provider adequado.
    /// </summary>
    public static class BindingValueProviderResolver
    {
        /// <summary>
        /// Mapeamento entre a origem do binding e seu respectivo provedor de valores.
        /// </summary>
        private static readonly Dictionary<BindingSource, IBindingValueProvider> _provider = new()
        {
            { BindingSource.Route, new RouteValueProvider() },
            { BindingSource.Query, new QueryValueProvider() }
        };

        /// <summary>
        /// Obtém o valor da requisição com base na origem definida na ação de binding.
        /// </summary>
        /// <param name="context">Contexto HTTP da requisição</param>
        /// <param name="action">Configuração da ação de binding</param>
        /// <returns>Valor obtido da requisição ou null caso não encontrado</returns>
        public static object? GetValue(HttpContext context, BinderAction action)
        {
            // Tenta localizar o provider correspondente à origem do binding
            if (_provider.TryGetValue(action.Source, out var provider))
                return provider.GetValue(context, action);

            // Caso não exista provider para a origem informada
            return null;
        }
    }
}
