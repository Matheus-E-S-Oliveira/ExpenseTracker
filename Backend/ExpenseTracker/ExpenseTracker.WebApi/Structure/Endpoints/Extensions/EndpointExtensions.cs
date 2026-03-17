using ExpenseTracker.WebApi.Structure.Endpoints.Interfaces;

namespace ExpenseTracker.WebApi.Structure.Endpoints.Extensions
{
    public static class EndpointExtensions
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = typeof(Program).Assembly
                .GetTypes()
                .Where(t => typeof(IEndpoint).IsAssignableFrom(t)
                            && !t.IsInterface
                            && !t.IsAbstract);

            foreach (var endpoint in endpoints)
            {
                var instace = (IEndpoint)Activator.CreateInstance(endpoint)!;
                instace.Map(app);
            }
        }
    }
}
