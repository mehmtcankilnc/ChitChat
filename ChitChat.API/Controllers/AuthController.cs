
using ChitChat.Application.Abstractions.Messaging;
using ChitChat.Application.Users.LoginUser;
using ChitChat.Application.Users.RegisterUser;
using ChitChat.Domain.Entities;
using ChitChat.Domain.Models;
using ChitChat.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
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

    [EnableRateLimiting("fixed")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand cmd, CancellationToken ct)
    {
        await _commands.Send(cmd, ct);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand cmd, CancellationToken ct)
    {
        return Ok(await _commands.Send<LoginUserCommand, UserLoginResponse>(cmd, ct));
    }

    [HttpGet]
    [Authorize]
    public async Task<List<User>> GetUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }
}
