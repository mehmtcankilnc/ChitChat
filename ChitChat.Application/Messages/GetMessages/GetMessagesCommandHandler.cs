using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Application.Abstractions.Persistence;
using ChitChat.Domain.Entities;

namespace ChitChat.Application.Messages.GetMessages;

public sealed class GetMessagesCommandHandler : ICommandHandler<GetMessagesCommand, List<Message>>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessagesCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<List<Message>> Handle(GetMessagesCommand command, CancellationToken cancellationToken)
    {
        return await _messageRepository.GetMessages(command, cancellationToken);
    }
}
