using Entities;

namespace Chat.API.Hubs.ChatHelpers
{
    public static class Messages
    {
        public static Message JoinRoomMessage(string username) => new Message
        {
            Body = $"{username} joined",
            CreatedAt = DateTime.UtcNow,
            SenderName = "ChatBot"
        };

        public static Message LeaveRoomMessage(string username) => new Message
        {
            Body = $"{username} left the chat",
            CreatedAt = DateTime.UtcNow,
            SenderName = "ChatBot"
        };

        public static Message GetBlockUserMessage(string username) => new Message
        {
            Body = $"{username} were blocked",
            CreatedAt = DateTime.UtcNow,
            SenderName = "ChatBot"
        };
    }
}
