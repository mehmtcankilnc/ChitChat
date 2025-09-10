
namespace ChitChat.Application.Exceptions;

public class EmailOrUsernameInUseException(string message) : Exception(message)
{
}
