using Microsoft.AspNetCore.SignalR;

namespace CursoIntroSignalRYoutube.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            await Clients.All.SendAsync($"ReceiveMessage{receiverId}", senderId, receiverId, message).ConfigureAwait(false);
        }
    }
}