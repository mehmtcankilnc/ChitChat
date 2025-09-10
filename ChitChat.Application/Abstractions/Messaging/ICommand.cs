
namespace ChitChat.Application.Abstractions.Messaging;

public interface ICommand : IBaseCommand
{
}

public interface ICommand<out TResponse> : IBaseCommand
{
}

public interface IBaseCommand
{
}
