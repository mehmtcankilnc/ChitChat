
namespace ChitChat.Domain.Entities;

public class Message
{
    public Guid MessageId { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Date  { get; set; }
}
