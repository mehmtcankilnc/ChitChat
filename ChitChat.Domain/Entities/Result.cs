
using System.Text.Json;

namespace ChitChat.Domain.Entities;

public class Result
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
