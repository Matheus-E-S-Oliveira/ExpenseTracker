using ExpenseTracker.WebApi.Structure.Binding.Actions;
using System.Reflection;

namespace ExpenseTracker.WebApi.Structure.Binding.Cache
{
    /// <summary>
    /// Representa o cache de binding de um endpoint.
    ///
    /// Armazena as ações necessárias para realizar o binding das propriedades,
    /// evitando a necessidade de recalcular essas informações a cada requisição.
    /// Utilizado pelo EndpointBinder para otimizar o processo de binding,
    /// reduzindo o uso de reflection em tempo de execução.
    /// </summary>
    public class BinderCache(List<BinderAction> actions, PropertyInfo? bodyProperty)
    {
        /// <summary>
        /// Lista de ações responsáveis por realizar o binding das propriedades do endpoint.
        /// </summary>
        public List<BinderAction> Actions { get; } = actions;

        /// <summary>
        /// Propriedade que representa o corpo da requisição (Body), caso exista.
        /// </summary>
        public PropertyInfo? BodyProperty { get; } = bodyProperty;
    }
}
