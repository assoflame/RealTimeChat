using Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public class MessageRepo : GenericRepo<Message>, IMessageRepo
    {
        public MessageRepo(IMongoCollection<Message> collection) : base(collection)
            { }
    }
}
