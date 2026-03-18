using ExpenseTracker.WebApi.Structure.Binding.Actions;
using ExpenseTracker.WebApi.Structure.Binding.Core;
using ExpenseTracker.WebApi.Structure.Binding.Factories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ExpenseTracker.WebApi.Structure.Binding.Cache
{
    /// <summary>
    /// Fábrica responsável por construir o cache de binding de um endpoint.
    ///
    /// Analisa as propriedades do tipo informado, identifica seus atributos
    /// de binding ([FromRoute], [FromQuery], [FromBody]) e cria as ações
    /// necessárias para realizar o binding de forma otimizada.
    /// Implementa um mecanismo de análise de metadados similar ao model binding
    /// do ASP.NET Core, com foco em performance e reutilização.
    /// </summary>
    public static class BinderCacheFactory
    {
        /// <summary>
        /// Constrói o cache de binding para um tipo de endpoint.
        ///
        /// O processo inclui:
        /// - Leitura das propriedades via reflection
        /// - Identificação da origem dos dados (Route, Query, Body)
        /// - Criação de setters dinâmicos
        /// - Definição da propriedade de body (se existir)
        /// </summary>
        /// <param name="type">Tipo do endpoint</param>
        /// <returns>Instância de BinderCache contendo as ações de binding</returns>
        public static BinderCache BuildCache(Type type)
        {
            var actions = new List<BinderAction>();
            PropertyInfo? bodyProperty = null;

            // Percorre todas as propriedades do endpoint
            foreach (var property in type.GetProperties())
            {
                // Cria o setter dinâmico para a propriedade
                var setter = SetterFactory.Create(property);

                // Verifica se a propriedade vem da rota
                var fromRoute = property.GetCustomAttribute<FromRouteAttribute>();

                if (fromRoute is not null)
                {
                    actions.Add(new BinderAction
                    {
                        Setter = setter,
                        Name = fromRoute.Name ?? property.Name,
                        PropertyType = property.PropertyType,
                        Source = BindingSource.Route
                    });

                    continue;
                }

                // Verifica se a propriedade vem da query string
                var fromQuery = property.GetCustomAttribute<FromQueryAttribute>();

                if (fromQuery is not null)
                {
                    actions.Add(new BinderAction
                    {
                        Setter = setter,
                        Name = fromQuery.Name ?? property.Name,
                        PropertyType = property.PropertyType,
                        Source = BindingSource.Query
                    });

                    continue;
                }

                // Verifica se a propriedade vem do corpo da requisição
                var fromBody = property.GetCustomAttribute<FromBodyAttribute>();

                if (fromBody is not null)
                {
                    // Garante que apenas uma propriedade seja marcada como FromBody
                    if (bodyProperty is not null)
                        throw new InvalidOperationException("Somente um [FromBody] é permitido.");

                    bodyProperty = property;
                }
            }

            // Retorna o cache com todas as ações configuradas
            return new BinderCache(actions, bodyProperty);
        }
    }
}
