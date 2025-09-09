
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
    private readonly RegisterUserCommandHandler _handler;
    private readonly AppDbContext _dbContext;

    public AuthController(RegisterUserCommandHandler handler, AppDbContext dbContext)
    {
        _handler = handler;
        _dbContext = dbContext;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand cmd, CancellationToken ct)
    {
        await _handler.Handle(cmd, ct);
        return NoContent();
    }

    [HttpGet]
    public async Task<List<User>> GetUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }
}
