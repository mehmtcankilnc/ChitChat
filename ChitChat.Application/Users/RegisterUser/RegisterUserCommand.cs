
using ChitChat.Application.Abstractions.Messaging;

namespace ChitChat.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string Username, string Email, string Password) : ICommand
{
}
