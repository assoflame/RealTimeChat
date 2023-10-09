using Chat.API.Hubs.ChatHelpers;
using Entities;
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

        private string _userId => Context?.User?.FindFirst("Id")?.Value
            ?? throw new ArgumentException();
        private string _username => Context?.User?.FindFirst("Nickname")?.Value
            ?? throw new ArgumentException();

        public ChatHub(IServiceManager serviceManager, IDictionary<string, UserConnection> connections)
        {
            _serviceManager = serviceManager;
            _connections = connections;
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var room = _connections[Context.ConnectionId].Room;
            await RemoveRoomUser(room);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendConnectedUsers(string room)
        {
            var roomUsers = _connections.Values
                .Where(connection => connection.Room.Equals(room))
                .Select(connection => connection.Nickname)
                .Distinct()
                .ToArray();

            await Clients.Group(room).SendAsync("RecieveRoomUsers", roomUsers);
        }

        public async Task JoinRoom(string room)
        {
            if(await _serviceManager.ChatService.UserHasRoomAccessAsync(room, _username))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room);
                if (_connections.Values
                    .Where(uc => uc.Nickname.Equals(_username) && uc.Room.Equals(room))
                    .Count() == 0)
                    await Clients.Group(room).SendAsync("RecieveMessage", JoinRoomMessage);

                _connections.TryAdd(Context.ConnectionId,
                    new UserConnection { Nickname = _username, Room = room });

                await Clients.Caller.SendAsync("RecieveCurrentRoom",
                    new
                    {
                        Room = room,
                        AdminRights = await _serviceManager.ChatService.UserHasRoomAdminRightsAsync(room, _username)
                    });
            }
        }

        public async Task LeaveRoom(string room)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
            await RemoveRoomUser(room);
        }

        public async Task CreateRoom(string room)
        {
            var createRoomResult = await _serviceManager.ChatService
                .TryCreateRoomAsync(room, _username);

            await Clients.Caller.SendAsync("RecieveCreateRoomResult", createRoomResult);
        }

        public async Task SendRoomMessage(string room, string message)
        {
            if (await _serviceManager.ChatService.UserHasRoomAccessAsync(room, _userId))
            {
                await _serviceManager.ChatService.SendMessageAsync(room, message, _username);
                await Clients.Group(room).SendAsync("RecieveMessage", new Message()
                {
                    CreatedAt = DateTime.UtcNow,
                    RoomName = room,
                    SenderName = _username,
                    Body = message
                });
            }
        }

        public async Task GetRoomMessages(string room)
        {
            if(await _serviceManager.ChatService.UserHasRoomAccessAsync(room, _userId))
            {
                var messages = await _serviceManager.ChatService.GetRoomMessages(room);
                await Clients.Caller.SendAsync("RecieveRoomMessages", messages);
            }
        }

        public async Task BlockRoomUser(string room, string userName)
        {
            await _serviceManager.ChatService.BlockUserAsync(room, _username, userName);
        }

        public async Task GetRooms()
        {
            var rooms = await _serviceManager.ChatService.GetRooms();

            await Clients.All.SendAsync("RecieveRooms", rooms);
        }

        private async Task RemoveRoomUser(string room)
        {
            if(_connections.Values
                .Where(userConnection => userConnection.Nickname.Equals(_username)
                                        && userConnection.Room.Equals(room))
                .Count() == 1)
            {
                await Clients.Group(room).SendAsync("RecieveMessage", LeaveRoomMessage);
            }
            _connections.Remove(Context.ConnectionId);
            await SendConnectedUsers(room);
        }

        private Message JoinRoomMessage => new Message
        {
            Body = $"{_username} joined",
            CreatedAt = DateTime.UtcNow,
            SenderName = "ChatBot"
        };

        private Message LeaveRoomMessage => new Message
        {
            Body = $"{_username} left the chat",
            CreatedAt = DateTime.UtcNow,
            SenderName = "ChatBot"
        };
    }
}
