
using ChitChat.Application.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace ChitChat.Infrastructure.Messaging;

public sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task Send<TCommand>(TCommand command, CancellationToken ct = default) where TCommand : ICommand
    {
        return _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>().Handle(command, ct);
    }

    public Task<TResponse> Send<TCommand, TResponse>(TCommand command, CancellationToken ct = default) where TCommand : ICommand<TResponse>
    {
        return _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResponse>>().Handle(command, ct);
    }
}
