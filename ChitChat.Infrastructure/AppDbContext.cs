
using ChitChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChitChat.Infrastructure;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}
