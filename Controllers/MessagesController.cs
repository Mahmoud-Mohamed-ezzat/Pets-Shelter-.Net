using Animal2.Dto.messages;
using Animal2.Hubs;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Animal2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly AnimalsContext _context;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserManager<IdentityUser> _userManager;

        public MessagesController(
            AnimalsContext context,
            IHubContext<ChatHub> hubContext,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        // GET: api/messages/shelters (for Adopters)
        [Authorize(Roles = "Adopter")]
        [HttpGet("shelters")]
        public async Task<IActionResult> GetShelters()
        {
            var shelters = await _userManager.GetUsersInRoleAsync("Shelterstaff");
            var shelterDtos = shelters.Select(s => new { id = s.Id, name = s.UserName }).ToList();
            return Ok(shelterDtos);
        }

        // GET: api/messages/adopters (for Shelterstaff)
        [Authorize(Roles = "Shelterstaff")]
        [HttpGet("adopters")]
        public async Task<IActionResult> GetAdopters()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var adopters = await _context.Messages
                .Where(m => m.ReceiverId == currentUserId)
                .Select(m => new { id = m.SenderId, name = m.Sender.UserName })
                .Distinct()
                .ToListAsync();

            return Ok(adopters);
        }

        // POST: api/messages (send a new message)
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] AddMessageDto addMessageDto)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var receiver = await _userManager.FindByIdAsync(addMessageDto.ReceiverId);

            if (receiver == null)
                return NotFound("Receiver not found.");

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = addMessageDto.ReceiverId,
                MessageContent = addMessageDto.MessageContent,
                CreatedAt = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // Notify both users via SignalR
            await _hubContext.Clients.Users(senderId, addMessageDto.ReceiverId)
                .SendAsync("ReceiveMessage", new MessageDto
                {
                    Id = message.Id,
                    senderId = message.SenderId,
                    receiverId = message.ReceiverId,
                    MessageContent = message.MessageContent,
                    CreatedAt = message.CreatedAt
                });

            return Ok(message.ToMessageDto());
        }

        // GET: api/messages (get conversation between two users)
        [HttpGet]
        public async Task<IActionResult> GetConversation(string otherUserId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var messages = await _context.Messages
                .Where(m => (m.SenderId == currentUserId && m.ReceiverId == otherUserId) ||
                            (m.SenderId == otherUserId && m.ReceiverId == currentUserId))
                .OrderBy(m => m.CreatedAt)
                .Select(m => m.ToMessageDto())
                .ToListAsync();

            return Ok(messages);
        }

        // PUT: api/messages/5 (update a message)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] UpdateMessageDto updateDto)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (message.SenderId != currentUserId)
                return Forbid(); // Only the sender can edit

            message.MessageContent = updateDto.MessageContent;
            message.IsEdited = true;
            await _context.SaveChangesAsync();

            return Ok(message.ToMessageDto());
        }

        // DELETE: api/messages/5 (delete a message)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (message.SenderId != currentUserId)
                return Forbid(); // Only the sender can delete

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}