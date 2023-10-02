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
    }
}
