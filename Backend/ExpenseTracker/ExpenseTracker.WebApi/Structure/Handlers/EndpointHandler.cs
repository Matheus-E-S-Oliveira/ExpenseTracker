using ExpenseTracker.WebApi.Structure.Handlers.Interfaces;

namespace ExpenseTracker.WebApi.Structure.Handlers
{
    public abstract class EndpointHandler<TEndpoint> : IEndpointHandler<TEndpoint>
    {
        public abstract Task<IResult> HandleAsync(TEndpoint request, CancellationToken cancellationToken);
    }
}
