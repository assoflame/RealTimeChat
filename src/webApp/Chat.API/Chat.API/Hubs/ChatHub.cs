using Chat.API.Hubs.ChatHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IServiceManager _serviceManager;
        private readonly IDictionary<string, UserConnection> _connections;

        private readonly string? _userId;
        private readonly string? _username;

        public ChatHub(IServiceManager serviceManager, IDictionary<string, UserConnection> connections)
        {
            _serviceManager = serviceManager;
            _connections = _connections ?? throw new ArgumentException();
            _userId = Context?.User?.FindFirst("Id")?.Value;
            _username = Context?.User?.FindFirst("Nickname")?.Value;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public Task RefreshRoomUsers(string roomName)
        {

        }

        public async Task JoinRoom(string roomName)
        {
            if(await _serviceManager.ChatService.UserHasRoomAccess(roomName, _userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                _connections.Add(Context.ConnectionId,
                    new UserConnection { Nickname = _username, RoomName = roomName });
            }
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            _connections.Add(Context.ConnectionId,
                new UserConnection { Nickname = _username, RoomName = roomName });
        }

        public async Task CreateRoom(string roomName)
        {
            _serviceManager.ChatService.TryCreateRoom(roomName, _userId);
        }

        public async Task SendRoomMessage(string roomName, string message)
        {
            if(await _serviceManager.ChatService.UserHasRoomAccess(roomName, _userId))
            {
                await Clients.Group(roomName).SendAsync("RecieveMessage", message);
            }
        }

        public async Task BlockRoomUser(string roomName, string userName)
        {
            if(await _serviceManager.ChatService.UserHasRoomAdminRights(roomName, _userId))
            {
                await _serviceManager.ChatService.BlockUser(roomName, userName);
            }
        }
    }
}
