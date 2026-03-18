using ExpenseTracker.WebApi.Structure.Binding.Actions;

namespace ExpenseTracker.WebApi.Structure.Binding.Providers.Interfaces
{
    /// <summary>
    /// Define o contrato para provedores de valores utilizados no processo de binding.
    ///
    /// Cada implementação é responsável por obter um valor da requisição HTTP
    /// com base em uma origem específica (ex: Route, Query, etc.).
    /// </summary>
    public interface IBindingValueProvider
    {
        /// <summary>
        /// Obtém o valor da requisição com base no contexto HTTP e na configuração da ação de binding.
        /// </summary>
        /// <param name="context">Contexto HTTP da requisição</param>
        /// <param name="action">Configuração da ação de binding</param>
        /// <returns>Valor obtido ou null caso não encontrado</returns>
        object? GetValue(HttpContext context, BinderAction action);
    }
}
