using ChitChat.Domain.Entities;
using ChitChat.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChitChat.API.Hubs
{
    public sealed class MessageHub(AppDbContext context) : Hub
    {
        private Guid GetUserId()
        {
            var id = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(id!);
        }

        //public async Task Connect(Guid senderId)
        //{
        //    Users.Add(Context.ConnectionId, senderId);
        //    User? user = await context.Users.FindAsync(senderId);

        //    if (user is not null)
        //    {
        //        user.Status = "Çevrimiçi";
        //        await context.SaveChangesAsync();

        //        await Clients.All.SendAsync("Users", user);
        //    }
        //}

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();

            User? user = await context.Users.FindAsync(userId);
            if (user is not null)
            {
                user.Status = "Çevrimiçi";
                await context.SaveChangesAsync();
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());

            await Clients.All.SendAsync("UserStatusChanged", userId, "Çevrimiçi");
            await base.OnConnectedAsync();
        }

        //public override async Task OnDisconnectedAsync(Exception? exception)
        //{
        //    Users.TryGetValue(Context.ConnectionId, out Guid senderId);
        //    Users.Remove(Context.ConnectionId);

        //    User? user = await context.Users.FindAsync(senderId);
        //    if (user is not null)
        //    {
        //        user.Status = "Çevrimdışı";
        //        await context.SaveChangesAsync();

        //        await Clients.All.SendAsync("Users", user);
        //    }
        //}

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            var userId = GetUserId();

            User? user = await context.Users.FindAsync(userId);
            if (user is not null)
            {
                user.Status = "ÇevrimDışı";
                await context.SaveChangesAsync();
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId.ToString());

            await Clients.All.SendAsync("UserStatusChanged", userId, "Çevrimdışı");
            await base.OnDisconnectedAsync(ex);
        }
    }
}
