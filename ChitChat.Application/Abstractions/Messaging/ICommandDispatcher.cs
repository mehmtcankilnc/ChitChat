
namespace ChitChat.Application.Abstractions.Messaging;

public interface ICommandDispatcher
{
    Task Send<TCommand>(TCommand command, CancellationToken ct = default) 
        where TCommand : ICommand;

    Task<TResponse> Send<TCommand, TResponse>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand<TResponse>;
}
