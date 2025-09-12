
using ChitChat.Application.Users.LoginUser;
using ChitChat.Domain.Entities;
using ChitChat.Domain.Models;

namespace ChitChat.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<UserLoginResponse> LoginUserAsync(LoginUserCommand loginReq, CancellationToken ct = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct = default);
    Task AddAsync(User user, CancellationToken ct = default);
}
