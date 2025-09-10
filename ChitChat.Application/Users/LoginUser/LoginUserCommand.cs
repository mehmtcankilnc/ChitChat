
using ChitChat.Application.Abstractions.Messaging;

namespace ChitChat.Application.Users.LoginUser;

public sealed record class LoginUserCommand(string Email, string Password) : ICommand<Guid>
{
}
