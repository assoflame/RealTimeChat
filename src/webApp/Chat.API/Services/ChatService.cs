using DataAccess.Interfaces;
using Entities.Models;
using Entities.Exceptions;
using Services.Interfaces;

namespace Services
{
    public class ChatService : IChatService
    {
        private readonly IRepoManager _repoManager;

        public ChatService(IRepoManager repoManager)
        {
            _repoManager = repoManager;
        }

        public async Task BlockUserAsync(string roomName, string username)
        {
            var user = (await _repoManager.Users
                .FindByConditionAsync(user => user.Nickname.Equals(username)))
                .FirstOrDefault();

            var room = (await _repoManager.Rooms
                .FindByConditionAsync(room => room.Name.Equals(roomName)))
                .FirstOrDefault();

            if (user is null || room is null)
                throw new ArgumentException();

            room.BlackList.Add(username);

            await _repoManager.Rooms.UpdateRoomAsync(roomName, room);
        }

        public async Task<bool> TryCreateRoomAsync(string roomName, string username)
        {
            var room = (await _repoManager.Rooms
                .FindByConditionAsync(room => room.Name.Equals(roomName)))
                .FirstOrDefault();

            if (room is not null)
                return false;

            room = new Room()
            {
                Name = roomName,
                CreatedAt = DateTime.UtcNow,
                Admins = new List<string> { username },
                BlackList = new List<string>()
            };

            await _repoManager.Rooms.CreateAsync(room);

            return true;
        }

        public async Task<bool> UserHasRoomAccessAsync(string roomName, string username)
        {
            var room = (await _repoManager.Rooms
                .FindByConditionAsync(room => room.Name.Equals(roomName)))
                .FirstOrDefault();

            if (room is null)
                throw new RoomNotFoundException(roomName);

            return !room.BlackList.Contains(username);
        }

        public async Task<bool> UserHasRoomAdminRightsAsync(string roomName, string username)
        {
            var room = (await _repoManager.Rooms
                .FindByConditionAsync(room => room.Name.Equals(roomName)))
                .FirstOrDefault();

            if (room is null)
                throw new RoomNotFoundException(roomName);

            return room.Admins.Contains(username);
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return await _repoManager.Rooms.FindAllAsync();
        }

        public async Task<IEnumerable<Message>> GetRoomMessagesAsync(string room)
        {
            return await _repoManager.Messages
                .FindByConditionAsync(message => message.RoomName.Equals(room));
        }

        public async Task SendMessageAsync(string room, string messageBody, string username)
        {
            var message = new Message()
            {
                CreatedAt = DateTime.UtcNow,
                RoomName = room,
                SenderName = username,
                Body = messageBody
            };

            await _repoManager.Messages.CreateAsync(message);
        }
    }
}
