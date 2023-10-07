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
        Task BlockUserAsync(string roomName, string adminName, string username);
        Task<IEnumerable<Room>> GetRooms();
        Task<IEnumerable<Message>> GetRoomMessages(string room);
        Task SendMessageAsync(string room, string message, string username);
    }
}
