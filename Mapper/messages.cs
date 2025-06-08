using Animal2.Dto.messages;
using Animal2.Models;
using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace Animal2.Mapper
{
    public static class messages
    {
        public static MessageDto ToMessageDto(this Message message)
        {
            return new MessageDto
            {
                Id = message.Id,
                MessageContent = message.MessageContent,
                senderName = message.Sender.UserName,
                senderId = message.SenderId,
                receiverId = message.ReceiverId,
                CreatedAt = message.CreatedAt,
            };
        }
        public static Message AddMessageDtoToMessage(this AddMessageDto addMessageDto)
        {
            return new Message
            {
                MessageContent = addMessageDto.MessageContent,
                SenderId = addMessageDto.SenderId,
                ReceiverId = addMessageDto.ReceiverId,
                CreatedAt = DateTime.UtcNow,
                IsEdited = false,
            };
        }
        public static AdopterDto adopterDto(this Message message)
        {
            return new AdopterDto
            {
                id = message.SenderId,
                Name = message.Sender.UserName,
            };
        }
    }
}

