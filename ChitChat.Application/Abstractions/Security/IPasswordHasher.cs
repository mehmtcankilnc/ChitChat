
namespace ChitChat.Application.Abstractions.Security;

public interface IPasswordHasher
{
    string Hash(string password);
}
