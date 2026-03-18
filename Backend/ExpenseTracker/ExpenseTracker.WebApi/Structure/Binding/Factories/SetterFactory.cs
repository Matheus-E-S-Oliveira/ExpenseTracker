using System.Linq.Expressions;
using System.Reflection;

namespace ExpenseTracker.WebApi.Structure.Binding.Factories
{
    /// <summary>
    /// Fábrica responsável por criar dinamicamente setters de propriedades.
    /// 
    /// Utiliza Expression Trees para gerar delegates compilados em tempo de execução,
    /// permitindo atribuir valores a propriedades de forma performática,
    /// evitando o uso de Reflection direta.
    /// Utilizado no processo de binding para mapear dinamicamente valores
    /// recebidos para propriedades de objetos.
    /// </summary>
    public static class SetterFactory
    {
        /// <summary>
        /// Cria um delegate que permite definir o valor de uma propriedade em um objeto.
        /// 
        /// O delegate retornado recebe:
        /// - instance: objeto alvo
        /// - value: valor a ser atribuído à propriedade
        /// </summary>
        /// <param name="propertyInfo">Informações da propriedade que será manipulada</param>
        /// <returns>Delegate do tipo Action&lt;object, object?&gt; para atribuição de valor</returns>
        public static Action<object, object?> Create(PropertyInfo propertyInfo)
        {
            // Parâmetro que representa a instância do objeto
            var instance = Expression.Parameter(typeof(object), "instance");

            // Parâmetro que representa o valor a ser atribuído
            var value = Expression.Parameter(typeof (object), "value");

            // Converte o objeto genérico para o tipo da classe da propriedade
            var instanceCast = Expression.Convert(instance, propertyInfo.DeclaringType!);

            // Converte o valor genérico para o tipo da propriedade
            var valueCast = Expression.Convert(value, propertyInfo.PropertyType);

            // Acessa a propriedade da instância
            var property = Expression.Property(instanceCast, propertyInfo);

            // Cria a expressão de atribuição (property = value)
            var assing = Expression.Assign(property, valueCast);

            // Compila a expressão em um delegate executável
            return Expression
                .Lambda<Action<object, object?>>(assing, instance, value)
                .Compile();
        }
    }
}
