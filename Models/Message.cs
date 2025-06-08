using System.ComponentModel.DataAnnotations.Schema;

namespace Animal2.Models
{
    [Table("Messages")]
    public class Message
    {
        public int Id { get; set; }
        public required string MessageContent { get; set; }
        public bool IsEdited { get; set; }
        [ForeignKey("SenderId")]
        public virtual Customer? Sender { get; set; }
        public string? SenderId { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual Customer? Receiver { get; set; }
        public string? ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
