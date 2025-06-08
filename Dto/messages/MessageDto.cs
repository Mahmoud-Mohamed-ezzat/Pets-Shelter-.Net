namespace Animal2.Dto.messages
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string MessageContent { get; set; }
        public string senderId { get; set; }
        public string receiverId { get; set; }
        public string senderName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
