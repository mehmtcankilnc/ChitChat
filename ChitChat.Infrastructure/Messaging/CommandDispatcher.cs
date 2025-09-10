
using ChitChat.Application.Abstractions.Messaging;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace ChitChat.Infrastructure.Messaging;

public sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Send<TCommand>(TCommand command, CancellationToken ct = default) where TCommand : ICommand
    {
        await Validate(command, ct);

        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.Handle(command, ct);
    }

    public async Task<TResponse> Send<TCommand, TResponse>(TCommand command, CancellationToken ct = default) where TCommand : ICommand<TResponse>
    {
        await Validate(command, ct);

        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResponse>>();
        return await handler.Handle(command, ct);
    }

    private async Task Validate<TCommand>(TCommand command, CancellationToken ct)
    {
        var validators = _serviceProvider.GetServices<IValidator<TCommand>>();
        if (validators is null) return;

        var context = new ValidationContext<TCommand>(command);
        List<ValidationFailure>? failures = [];

        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(context, ct);
            if (!result.IsValid)
            {
                failures.AddRange(result.Errors);
            }
        }

        if (failures is not null && failures.Count > 0)
            throw new ChitChat.Application.Exceptions.ValidationException(failures);
    }
}
