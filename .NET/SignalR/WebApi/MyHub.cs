using Microsoft.AspNetCore.SignalR;

internal class MyHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.Others.SendAsync("ReceiveMessage", message);
    }
}