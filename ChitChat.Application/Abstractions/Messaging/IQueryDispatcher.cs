
namespace ChitChat.Application.Abstractions.Messaging;

public interface IQueryDispatcher
{
    Task<TResponse> Send<TQuery, TResponse>(TQuery query, CancellationToken ct = default)
        where TQuery : IQuery<TResponse>;
}
