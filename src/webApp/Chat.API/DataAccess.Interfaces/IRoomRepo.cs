﻿using Entities.Models;

namespace DataAccess.Interfaces
{
    public interface IRoomRepo : IGenericRepo<Room>
    {
        Task UpdateRoomAsync(string roomName, Room updateRoom);
    }
}
