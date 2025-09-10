
using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Application.Abstractions.Persistence;
using ChitChat.Application.Abstractions.Security;
using ChitChat.Application.Exceptions;

namespace ChitChat.Application.Users.LoginUser;

public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user == null || !_passwordHasher.Verify(command.Password, user.PasswordHash))
            throw new InvalidCredentialsException("Geçersiz eposta veya şifre!");

        return user.UserId;
    }
}
