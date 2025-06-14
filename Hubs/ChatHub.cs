using Microsoft.AspNetCore.SignalR;
using Animal2.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class MessageHub : Hub
{
    private readonly AnimalsContext _context;

    public MessageHub(AnimalsContext context)
    {
        _context = context;
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public async Task JoinUserGroup(string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
    }

    public async Task SendMessageToUser(string senderId, string receiverId, string messageContent)
    {
        var message = new Message
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            MessageContent = messageContent,
            CreatedAt = DateTime.Now
        };
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        await Clients.Group(receiverId).SendAsync("ReceiveMessage", message.Id, senderId, messageContent);
    }

    public async Task EditMessage(int messageId, string newContent)
    {
        var message = await _context.Messages.FindAsync(messageId);
        if (message == null) return;
        message.MessageContent = newContent;
        await _context.SaveChangesAsync();

        await Clients.Users(message.SenderId, message.ReceiverId)
                   .SendAsync("MessageEdited", messageId, newContent);
    }

    public async Task DeleteMessage(int messageId)
    {
        var message = await _context.Messages.FindAsync(messageId);
        if (message == null) return;

        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();

        await Clients.Users(message.SenderId, message.ReceiverId)
                   .SendAsync("MessageDeleted", messageId);
    }
}