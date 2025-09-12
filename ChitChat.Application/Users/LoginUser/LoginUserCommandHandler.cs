
using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Application.Abstractions.Persistence;
using ChitChat.Application.Abstractions.Security;
using ChitChat.Application.Exceptions;
using ChitChat.Domain.Models;

namespace ChitChat.Application.Users.LoginUser;

public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, UserLoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public LoginUserCommandHandler(
        IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserLoginResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user == null || !_passwordHasher.Verify(command.Password, user.PasswordHash))
            throw new InvalidCredentialsException("Geçersiz eposta veya şifre!");

        var loginRes = await _userRepository.LoginUserAsync(command, cancellationToken);
        
        user.Status = "Çevrimiçi";
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return loginRes;
    }
}
