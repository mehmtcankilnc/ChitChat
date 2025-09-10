
using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Application.Abstractions.Persistence;
using ChitChat.Application.Abstractions.Security;
using ChitChat.Application.Users.LoginUser;
using ChitChat.Application.Users.RegisterUser;
using ChitChat.Infrastructure.Messaging;
using ChitChat.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChitChat.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>();
        services.AddScoped<ICommandHandler<LoginUserCommand, Guid>, LoginUserCommandHandler>();
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
