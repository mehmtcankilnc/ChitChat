
using ChitChat.Application.Abstractions.Persistence;
using ChitChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChitChat.Infrastructure.Services;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        await _appDbContext.Users.AddAsync(user, ct);
    }

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default) =>
        _appDbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, ct);

    public Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct = default) =>
        _appDbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Username == username, ct);

    public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
        _appDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, ct);
}
