
using ChitChat.Domain.Models;

namespace ChitChat.Application.Abstractions.Persistence;

public interface ITokenService
{
    Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request);
}
