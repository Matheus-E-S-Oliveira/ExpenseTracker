namespace ExpenseTracker.WebApi.Structure.Handlers.Interfaces
{
    public interface IEndpointHandler<TEndpoint>
    {
        Task<IResult> HandleAsync(TEndpoint request, CancellationToken cancellationToken);
    }
}
