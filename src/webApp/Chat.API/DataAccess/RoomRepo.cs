using Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public class RoomRepo : GenericRepo<Room>, IRoomRepo
    {
        public RoomRepo(IMongoCollection<Room> collection) : base(collection)
            { }

        public async Task UpdateRoomAsync(string roomName, Room updateRoom)
            => await _collection.ReplaceOneAsync(room => room.Name == roomName, updateRoom);
    }
}
