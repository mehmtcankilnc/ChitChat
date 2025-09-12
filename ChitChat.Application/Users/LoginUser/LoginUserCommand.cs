
using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Domain.Models;

namespace ChitChat.Application.Users.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<UserLoginResponse>
{
}
