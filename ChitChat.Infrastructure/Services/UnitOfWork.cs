
using ChitChat.Application.Abstractions.Persistence;

namespace ChitChat.Infrastructure.Services;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _appDbContext.SaveChangesAsync(ct);
    }
}
