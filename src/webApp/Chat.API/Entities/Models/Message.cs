namespace Entities.Models
{
    public class Message : BaseEntity
    {
        public string SenderName { get; set; }
        public string RoomName { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
