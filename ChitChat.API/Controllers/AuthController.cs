
using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Application.Users.LoginUser;
using ChitChat.Application.Users.RegisterUser;
using ChitChat.Domain.Entities;
using ChitChat.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChitChat.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ICommandDispatcher _commands;
    private readonly AppDbContext _dbContext;

    public AuthController(ICommandDispatcher commands, AppDbContext dbContext)
    {
        _commands = commands;
        _dbContext = dbContext;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand cmd, CancellationToken ct)
    {
        await _commands.Send(cmd, ct);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand cmd, CancellationToken ct)
    {
        Guid userId = await _commands.Send<LoginUserCommand, Guid>(cmd, ct);
        return Ok(userId);
    }

    [HttpGet]
    public async Task<List<User>> GetUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }
}
