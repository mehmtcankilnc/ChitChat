
using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Application.Abstractions.Persistence;
using ChitChat.Application.Abstractions.Security;
using ChitChat.Domain.Entities;

namespace ChitChat.Application.Users.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler
        (IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmailAsync(command.Email, cancellationToken) ||
            await _userRepository.ExistsByUsernameAsync(command.Username, cancellationToken))
        {
            throw new Exception("Email or username is already in use!");
        }

        var passwordHash = _passwordHasher.Hash(command.Password);

        var user = new User(
            username: command.Username,
            email: command.Email,
            passwordHash: passwordHash
        )
        {
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return;
    }
}
