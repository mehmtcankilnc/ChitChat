
using ChitChat.Application.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace ChitChat.Infrastructure.Messaging;

public sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResponse> Send<TQuery, TResponse>(TQuery query, CancellationToken ct = default) where TQuery : IQuery<TResponse>
    {
        return _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>().Handle(query, ct);
    }
}
