
using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Domain.Entities;

namespace ChitChat.Application.Messages.SendMessage;

public sealed record SendMessageCommand(Guid SenderId, Guid ReceiverId, string Content) : ICommand<Message>
{
}
