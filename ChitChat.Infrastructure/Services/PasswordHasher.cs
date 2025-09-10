
using ChitChat.Application.Abstractions.Security;

namespace ChitChat.Infrastructure.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return password; //implemente edilcek.
    }

    public bool Verify(string password, string hashedPassword)
    {
        return password == hashedPassword;
    }
}
