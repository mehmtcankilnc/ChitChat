using ChitChat.Application.Messages.GetMessages;
using ChitChat.Application.Messages.SendMessage;
using ChitChat.Domain.Entities;

namespace ChitChat.Application.Abstractions.Persistence;

public interface IMessageRepository
{
    public Task<List<Message>> GetMessages(GetMessagesCommand command, CancellationToken ct = default);
    public Task<Message> SendMessage(SendMessageCommand command, CancellationToken ct = default);
}
