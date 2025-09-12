
using ChitChat.Application.Abstractions.Persistence;
using ChitChat.Application.Users.LoginUser;
using ChitChat.Domain.Entities;
using ChitChat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChitChat.Infrastructure.Services;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ITokenService _tokenService;

    public UserRepository(AppDbContext appDbContext, ITokenService tokenService)
    {
        _appDbContext = appDbContext;
        _tokenService = tokenService;
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
            .FirstOrDefaultAsync(u => u.Email == email, ct);

    public async Task<UserLoginResponse> LoginUserAsync(LoginUserCommand loginReq, CancellationToken ct = default)
    {
        UserLoginResponse loginResponse = new();

        var tokenInfo = await _tokenService.GenerateToken(new GenerateTokenRequest { Email = loginReq.Email });

        loginResponse.AuthenticateResult = true;
        loginResponse.AuthToken = tokenInfo.Token;
        loginResponse.AccessTokenExpireDate = tokenInfo.TokenExpireDate;

        return await Task.FromResult(loginResponse);
    }
}
