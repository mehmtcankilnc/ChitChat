
using ChitChat.Application.Abstractions.Security;
using System.Security.Cryptography;

namespace ChitChat.Infrastructure.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return password; //implemente edilcek.
    }
}
