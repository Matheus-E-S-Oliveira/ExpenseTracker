using ExpenseTracker.WebApi.Structure.Binding.Core;

namespace ExpenseTracker.WebApi.Structure.Binding.Actions
{
    /// <summary>
    /// Representa uma ação de binding para uma propriedade do endpoint.
    ///
    /// Contém as informações necessárias para:
    /// - Obter o valor da requisição
    /// - Converter o valor para o tipo correto
    /// - Atribuir o valor à propriedade do endpoint
    /// Utilizada pelo EndpointBinder para executar o processo de binding de forma dinâmica.
    /// </summary>
    public class BinderAction
    {
        /// <summary>
        /// Delegate responsável por definir o valor na propriedade do endpoint.
        /// Gerado dinamicamente via Expression Tree.
        /// </summary>
        public Action<object, object?> Setter { get; set; } = default!;

        /// <summary>
        /// Nome da propriedade ou chave utilizada para buscar o valor na requisição.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Tipo da propriedade que será utilizada para conversão do valor.
        /// </summary>
        public Type PropertyType { get; set; } = default!;

        /// <summary>
        /// Origem do valor na requisição (Query, Route, Header, etc.).
        /// </summary>
        public BindingSource Source { get; set; }
    }
}
