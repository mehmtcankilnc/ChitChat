
using ChitChat.Application.Abstractions.Persistence;
using ChitChat.Application.Messages.GetMessages;
using ChitChat.Application.Messages.SendMessage;
using ChitChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChitChat.Infrastructure.Services;

public sealed class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _context;

    public MessageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Message>> GetMessages(GetMessagesCommand command, CancellationToken ct = default)
    {
        return await _context.Messages.Where(
            m => 
                m.SenderId == command.SenderId && m.ReceiverId == command.ReceiverId ||
                m.SenderId == command.ReceiverId && m.ReceiverId == command.SenderId)
            .OrderBy(m => m.Date).ToListAsync(ct);
    }

    public async Task<Message> SendMessage(SendMessageCommand command, CancellationToken ct = default)
    {
        Message message = new()
        {
            SenderId = command.SenderId,
            ReceiverId = command.ReceiverId,
            Content = command.Content,
            Date = DateTime.UtcNow,
        };

        await _context.Messages.AddAsync(message, ct);
        return message;
    }
}
