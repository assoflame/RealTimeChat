using Chat.API.Hubs.ChatHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Services.Interfaces;

namespace Chat.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IServiceManager _serviceManager;
        private readonly IDictionary<string, UserConnection> _connections;

        private readonly string _userId;
        private readonly string _username;

        public ChatHub(IServiceManager serviceManager, IDictionary<string, UserConnection> connections)
        {
            _serviceManager = serviceManager;
            _connections = connections;
            _userId = Context?.User?.FindFirst("Id")?.Value ?? throw new ArgumentException();
            _username = Context?.User?.FindFirst("Nickname")?.Value ?? throw new ArgumentException();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendConnectedUsers(string room)
        {
            var roomUsers = _connections.Values
                .Where(connection => connection.RoomName.Equals(room))
                .Select(connection => connection.Nickname);

            await Clients.Group(room).SendAsync("RecieveRoomUsers", roomUsers);
        }

        public async Task JoinRoom(string room)
        {
            if(await _serviceManager.ChatService.UserHasRoomAccessAsync(room, _username))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room);
                _connections.Add(Context.ConnectionId,
                    new UserConnection { Nickname = _username, RoomName = room });
            }
        }

        public async Task LeaveRoom(string room)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
            _connections.Add(Context.ConnectionId,
                new UserConnection { Nickname = _username, RoomName = room });
        }

        public async Task CreateRoom(string room)
        {
            await _serviceManager.ChatService.TryCreateRoomAsync(room, _userId);
        }

        public async Task SendRoomMessage(string room, string message)
        {
            if(await _serviceManager.ChatService.UserHasRoomAccessAsync(room, _userId))
            {
                await Clients.Group(room).SendAsync("RecieveMessage", message);
            }
        }

        public async Task BlockRoomUser(string room, string userName)
        {
            await _serviceManager.ChatService.BlockUserAsync(room, _username, userName);
        }
    }
}
