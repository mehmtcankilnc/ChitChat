using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Domain.Entities;

namespace ChitChat.Application.Messages.GetMessages;

public sealed record GetMessagesCommand(Guid SenderId, Guid ReceiverId) : ICommand<List<Message>>
{
}
