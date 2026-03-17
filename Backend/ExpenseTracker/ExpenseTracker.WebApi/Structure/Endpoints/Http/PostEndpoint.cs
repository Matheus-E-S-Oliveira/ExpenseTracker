
namespace ExpenseTracker.WebApi.Structure.Endpoints.Http
{
    public abstract class PostEndpoint<TEndpoint, TResponse>(IServiceProvider serviceProvider) : BaseEndpoint<TEndpoint, TResponse>(serviceProvider)
        where TEndpoint : BaseEndpoint<TEndpoint, TResponse>
    {
        protected void Post(string route)
        {
            Route(route);
        }

        protected override void MapMethod(IEndpointRouteBuilder app, string route, Delegate handler)
        {
            app.MapPost(route, handler);
        }
    }
}
