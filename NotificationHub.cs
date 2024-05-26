using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Firebase.Api;

[Authorize]
public class NotificationHub : Hub<INotificationClient>
{

    public override Task OnConnectedAsync()
    {
        Clients.Client(Context.ConnectionId).RecibeNotification(
            $"Gracias por tomar este curso {Context.User?.Identity?.Name}"
        );
                
        return base.OnConnectedAsync();
    }

}