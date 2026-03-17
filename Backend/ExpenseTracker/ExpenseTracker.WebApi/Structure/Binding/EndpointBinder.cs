using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ExpenseTracker.WebApi.Structure.Binding
{
    public static class EndpointBinder
    {
        private static readonly Dictionary<Type, BinderCache> _cache = [];

        public static async Task BindAsync<TEndpoint>(TEndpoint endpoint, HttpContext context, CancellationToken cancellationToken)
        {
            var type = typeof(TEndpoint);

            if (!_cache.TryGetValue(type, out var cache))
            {
                cache = BuildCache(type);
                _cache[type] = cache;
            }

            foreach (var action in cache.Actions)
            {
                object? rawValue = null;

                switch (action.Source)
                {
                    case BindingSource.Route:
                        if (context.Request.RouteValues.TryGetValue(action.Name, out var routeVal))
                            rawValue = routeVal;
                        break;

                    case BindingSource.Query:
                        var queryVal = context.Request.Query[action.Name];
                        if (!string.IsNullOrWhiteSpace(queryVal))
                            rawValue = queryVal.ToString();
                        break;
                }

                if (rawValue is null) continue;

                var converted = TypeConverter.ConvertTo(rawValue, action.PropertyType);

                if (converted is not null) action.Setter(endpoint!, converted);
            }

            if (cache.BodyProperty is not null)
                await BindBody(endpoint!, context, cache.BodyProperty, cancellationToken);
        }

        private static BinderCache BuildCache(Type type)
        {
            var actions = new List<BinderAction>();
            PropertyInfo? bodyProperty = null;

            foreach (var property in type.GetProperties())
            {
                var setter = SetterFactory.Create(property);

                var fromRoute = property.GetCustomAttribute<FromRouteAttribute>();
                
                if (fromRoute is not null)
                {
                    actions.Add(new BinderAction
                    {
                        Setter = setter,
                        Name = (fromRoute.Name ?? property.Name),
                        PropertyType = property.PropertyType,
                        Source = BindingSource.Route
                    });

                    continue;
                }

                var fromQuery = property.GetCustomAttribute<FromQueryAttribute>();

                if (fromQuery is not null)
                {
                    actions.Add(new BinderAction
                    {
                        Setter = setter,
                        Name = (fromQuery.Name ?? property.Name),
                        PropertyType = property.PropertyType,
                        Source = BindingSource.Query
                    });

                    continue;
                }

                var fromBody = property.GetCustomAttribute<FromBodyAttribute>();

                if (fromBody is not null)
                {
                    if (bodyProperty is not null)
                        throw new InvalidOperationException("Somente um [FromBody] é permitido.");

                    bodyProperty = property;
                }
            }

            return new BinderCache(actions, bodyProperty);
        }

        private static async Task BindBody(object endpoint, HttpContext context, PropertyInfo property, CancellationToken cancellationToken)
        {
            if (!context.Request.HasJsonContentType()) return;

            var body  = await context.Request.ReadFromJsonAsync(property.PropertyType, cancellationToken);

            if (body is not null)
                property.SetValue(endpoint, body);
        }

        private class BinderCache
        {
            public List<BinderAction> Actions { get; }
            public PropertyInfo? BodyProperty { get; }

            public BinderCache(List<BinderAction> actions, PropertyInfo? bodyProperty)
            {
                Actions = actions;
                BodyProperty = bodyProperty;
            }
        }

        private class BinderAction
        {
            public Action<object, object?> Setter { get; set; } = default!;

            public string Name { get; set; } = default!;

            public Type PropertyType { get; set; } = default!;

            public BindingSource Source { get; set; }
        }

        private enum BindingSource
        {
            Route,
            Query
        }
    }
}
