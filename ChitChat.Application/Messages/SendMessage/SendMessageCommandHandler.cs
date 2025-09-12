
using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Application.Abstractions.Persistence;
using ChitChat.Domain.Entities;

namespace ChitChat.Application.Messages.SendMessage;

public sealed class SendMessageCommandHandler : ICommandHandler<SendMessageCommand, Message>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendMessageCommandHandler(IMessageRepository messageRepository, IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Message> Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        var msg = await _messageRepository.SendMessage(command, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return msg;
    }
}
