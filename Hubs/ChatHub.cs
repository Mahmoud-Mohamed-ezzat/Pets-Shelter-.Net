using Animal2.Models;
using Microsoft.AspNetCore.SignalR;

namespace Animal2.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AnimalsContext _context;

        public ChatHub(AnimalsContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string senderId, string receiverId, string messageContent)
        {
            var Date = DateTime.Now;
            var message = new Message
            {
                CreatedAt = Date,
                SenderId = senderId,
                ReceiverId = receiverId,
                MessageContent = messageContent
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            List<string> Customers = new List<string>()
            {
                receiverId,senderId
            };

            await Clients.Users(Customers).SendAsync("ReceiveMessage", message.Id, message.SenderId, message.ReceiverId, message.MessageContent, message.CreatedAt);
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }
}

