using ExpenseTracker.WebApi.Structure.Endpoints.Interfaces;
using ExpenseTracker.WebApi.Structure.Handlers.Interfaces;

namespace ExpenseTracker.WebApi.Structure.Endpoints
{
    public abstract class BaseEndpoint<TEndpoint, TResponse>(IServiceProvider serviceProvider) : IEndpoint
        where TEndpoint : BaseEndpoint<TEndpoint, TResponse>
    {
        private string _route = "";

        protected void Route(string route)
        {
            _route = route;
        }

        protected abstract void Configure();

        protected abstract void MapMethod(IEndpointRouteBuilder app, string route, Delegate handler);

        public void Map(IEndpointRouteBuilder app)
        {
            Configure();

            if (string.IsNullOrWhiteSpace(_route))
                throw new InvalidOperationException("A rota não foi definida");

            MapMethod(app, _route, BuildHandler());
        }

        protected Delegate BuildHandler()
        {
            return async (HttpContext context, CancellationToken cancellationToken) =>
            {
                var endpoint = context.RequestServices.GetRequiredService<TEndpoint>();

                await BindAsync(endpoint, context);

                //var validator = context.RequestServices.GetService<IValidator<TEndpoint>>();

                //if (validator is not null)
                //{
                //    var result = await validator.ValidateAsync(endpoint, cancellationToken);

                //    if (!result.IsValid)
                //        return Results.BadRequest(result.Errors);
                //}

                var handler = context.RequestServices.GetRequiredService<IEndpointHandler<TEndpoint>>();

                var response = await handler.HandleAsync(endpoint, cancellationToken);

                return response;
            };
        }

        private static async Task BindAsync(TEndpoint endpoint, HttpContext context)
        {
            if (context.Request.HasJsonContentType())
            {
                var body = await context.Request.ReadFromJsonAsync(
                    endpoint.GetType().GetProperty("Body")!.PropertyType);

                endpoint.GetType().GetProperty("Body")!
                    .SetValue(endpoint, body);
            }
        }
    }
}
