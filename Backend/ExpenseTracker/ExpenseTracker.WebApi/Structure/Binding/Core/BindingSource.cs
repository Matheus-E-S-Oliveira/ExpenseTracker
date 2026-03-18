namespace ExpenseTracker.WebApi.Structure.Binding.Core
{
    /// <summary>
    /// Representa a origem dos dados utilizados no processo de binding.
    /// </summary>
    public enum BindingSource
    {
        /// <summary>
        /// Indica que o valor deve ser obtido a partir da rota da requisição (URL).
        /// Exemplo: /gastos/{id}
        /// </summary>
        Route,

        /// <summary>
        /// Indica que o valor deve ser obtido a partir da query string da requisição.
        /// Exemplo: /gastos?pagina=1
        /// </summary>
        Query
    }
}
