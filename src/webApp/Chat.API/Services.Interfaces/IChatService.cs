using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IChatService
    {
        Task<bool> UserHasRoomAccessAsync(string room, string username);
        Task<bool> TryCreateRoomAsync(string room, string username);
        Task BlockUserAsync(string roomName, string username);
        Task<IEnumerable<Room>> GetRoomsAsync();
        Task<IEnumerable<Message>> GetRoomMessagesAsync(string room);
        Task SendMessageAsync(string room, string message, string username);
        Task<bool> UserHasRoomAdminRightsAsync(string roomName, string username);
    }
}
