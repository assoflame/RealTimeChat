using Chat.API.Hubs.ChatHelpers;
using Entities.Models;
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
            if(_connections.TryGetValue(Context.ConnectionId, out var userConnection))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userConnection.Room);

                if (IsUserInOnlySingleRoom(_username, userConnection.Room))
                {
                    await Clients.Group(userConnection.Room).SendAsync("RecieveMessage", Messages.LeaveRoomMessage(_username));
                }
                _connections.Remove(Context.ConnectionId);
                await SendConnectedUsers(userConnection.Room);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendConnectedUsers(string room)
        {
            if (await _serviceManager.ChatService.UserHasRoomAccessAsync(room, _username))
            {
                var roomUsers = _connections.Values
                    .Where(connection => connection.Room.Equals(room))
                    .Select(connection => connection.Nickname)
                    .Distinct()
                    .ToArray();

                await Clients.Group(room).SendAsync("RecieveRoomUsers", roomUsers);
            }
        }

        public async Task JoinRoom(string room)
        {
            if(await _serviceManager.ChatService.UserHasRoomAccessAsync(room, _username))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room);

                if (IsUserFirstRoomConnection(_username, room))
                    await Clients.Group(room).SendAsync("RecieveMessage", Messages.JoinRoomMessage(_username));

                _connections.TryAdd(Context.ConnectionId,
                    new UserConnection { Nickname = _username, Room = room });

                await Clients.Caller.SendAsync("RecieveCurrentRoom",
                    new
                    {
                        Room = room,
                        AdminRights = await _serviceManager.ChatService.UserHasRoomAdminRightsAsync(room, _username)
                    });

                await SendConnectedUsers(room);
            }
        }

        public async Task LeaveRoom(string room)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);

            if (IsUserInOnlySingleRoom(_username, room))
            {
                await Clients.Group(room).SendAsync("RecieveMessage", Messages.LeaveRoomMessage(_username));
            }
            _connections.Remove(Context.ConnectionId);
            await SendConnectedUsers(room);
        }

        public async Task CreateRoom(string room)
        {
            var createRoomResult = await _serviceManager.ChatService
                .TryCreateRoomAsync(room, _username);

            await Clients.Caller.SendAsync("RecieveCreateRoomResult", createRoomResult);
        }

        public async Task SendRoomMessage(string room, string message)
        {
            if (await _serviceManager.ChatService.UserHasRoomAccessAsync(room, _username))
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
            if(await _serviceManager.ChatService.UserHasRoomAccessAsync(room, _username))
            {
                var messages = await _serviceManager.ChatService.GetRoomMessagesAsync(room);
                await Clients.Caller.SendAsync("RecieveRoomMessages", messages);
            }
        }

        public async Task BlockRoomUser(string room, string username)
        {
            if(await _serviceManager.ChatService.UserHasRoomAdminRightsAsync(room, _username))
            {
                await _serviceManager.ChatService.BlockUserAsync(room, username);
                await Clients.Group(room).SendAsync("RecieveMessage", Messages.GetBlockUserMessage(username));
                await RemoveAllConnections(username, room);

                await SendConnectedUsers(room);
            }
        }

        public async Task GetRooms()
        {
            var rooms = await _serviceManager.ChatService.GetRoomsAsync();

            await Clients.All.SendAsync("RecieveRooms", rooms);
        }

        private bool IsUserInOnlySingleRoom(string username, string room)
        {
            return _connections.Values
                .Where(userConnection => userConnection.Nickname.Equals(username)
                                        && userConnection.Room.Equals(room))
                .Count() == 1;
        }

        private bool IsUserFirstRoomConnection(string username, string room)
        {
            return _connections.Values
                    .Where(userConnection => userConnection.Nickname.Equals(username) 
                                            && userConnection.Room.Equals(room))
                    .Count() == 0;
        }

        private async Task RemoveAllConnections(string username, string room)
        {
            var connectionIds = _connections
                .Where(pair => pair.Value.Nickname.Equals(username)
                              && pair.Value.Room.Equals(room))
                .Select(pair => pair.Key)
                .ToArray();

            foreach (var connectionId in connectionIds)
            {
                _connections.Remove(connectionId);
                await Groups.RemoveFromGroupAsync(connectionId, room);
            }
        }
    }
}
