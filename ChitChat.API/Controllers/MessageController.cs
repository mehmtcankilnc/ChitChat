using ChitChat.API.Hubs;
using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Application.Messages.GetMessages;
using ChitChat.Application.Messages.SendMessage;
using ChitChat.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChitChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ICommandDispatcher _commands;
        private readonly IHubContext<MessageHub> _hub;

        public MessageController(ICommandDispatcher commands, IHubContext<MessageHub> hub)
        {
            _commands = commands;
            _hub = hub;
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetMessages(GetMessagesCommand cmd, CancellationToken ct)
        {
            return Ok(await _commands.Send<GetMessagesCommand, List<Message>>(cmd, ct));
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(SendMessageCommand cmd, CancellationToken ct)
        {
            var message = await _commands.Send<SendMessageCommand, Message>(cmd, ct);

            await _hub.Clients
              .Group(message.ReceiverId.ToString())
              .SendAsync("Messages", message, ct);

            return Ok();
        }
    }
}
