using AspNetCoreRateLimit;
using ChitChat.API.Extensions;
using ChitChat.API.Hubs;
using ChitChat.API.Middlewares;
using ChitChat.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AzureSql")));
builder.Services.AddInfrastructure();

builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimiting();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseIpRateLimiting();
app.UseAuthorization();

app.MapControllers();

app.MapHub<MessageHub>("/message-hub");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    for (int i = 1; i <= 10; i++)
    {
        try { await db.Database.MigrateAsync(); break; }
        catch { await Task.Delay(TimeSpan.FromSeconds(5)); }
    }
}


app.Run();
