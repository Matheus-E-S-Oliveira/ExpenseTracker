using ExpenseTracker.WebApi.Structure.Binding.Body;
using ExpenseTracker.WebApi.Structure.Binding.Cache;
using ExpenseTracker.WebApi.Structure.Binding.Converters;
using ExpenseTracker.WebApi.Structure.Binding.Providers;

namespace ExpenseTracker.WebApi.Structure.Binding.Core
{
    /// <summary>
    /// Responsável por realizar o binding dos dados da requisição HTTP
    /// para as propriedades de um endpoint.
    ///
    /// Utiliza cache para evitar recomputação de metadados e melhora de performance,
    /// além de conversão dinâmica de tipos e suporte a binding de corpo da requisição.
    /// Implementa um mecanismo de binding customizado similar ao model binding do ASP.NET,
    /// com foco em performance e flexibilidade.
    /// </summary>
    public static class EndpointBinder
    {
        /// <summary>
        /// Cache de binders por tipo de endpoint.
        /// Evita recriar a configuração de binding a cada requisição.
        /// </summary>
        private static readonly Dictionary<Type, BinderCache> _cache = [];

        /// <summary>
        /// Realiza o binding dos dados da requisição para a instância do endpoint.
        ///
        /// O processo inclui:
        /// - Obtenção dos valores da requisição (query, route, headers, etc.)
        /// - Conversão para o tipo da propriedade
        /// - Atribuição dinâmica via setters gerados
        /// - Binding do corpo da requisição (se aplicável)
        /// </summary>
        /// <typeparam name="TEndpoint">Tipo do endpoint</typeparam>
        /// <param name="endpoint">Instância do endpoint a ser populada</param>
        /// <param name="context">Contexto HTTP da requisição</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        public static async Task BindAsync<TEndpoint>(TEndpoint endpoint, HttpContext context, CancellationToken cancellationToken)
        {
            var type = typeof(TEndpoint);

            // Verifica se já existe cache para o tipo do endpoint
            if (!_cache.TryGetValue(type, out var cache))
            {
                // Cria o cache com base nas propriedades do endpoint
                cache = BinderCacheFactory.BuildCache(type);
                _cache[type] = cache;
            }

            // Percorre todas as ações de binding configuradas
            foreach (var action in cache.Actions)
            {
                // Obtém o valor bruto da requisição (query, route, etc.)
                var rawValue = BindingValueProviderResolver.GetValue(context, action);

                // Ignora se não houver valor
                if (rawValue is null) continue;

                // Converte o valor para o tipo da propriedade
                var converted = TypeConverter.ConvertTo(rawValue, action.PropertyType);

                // Aplica o valor convertido na propriedade do endpoint
                if (converted is not null) action.Setter(endpoint!, converted);
            }

            // Realiza o binding do corpo da requisição, se existir
            if (cache.BodyProperty is not null)
                await BodyBinder.BindAsync(endpoint!, context, cache.BodyProperty, cancellationToken);
        }
    }
}
