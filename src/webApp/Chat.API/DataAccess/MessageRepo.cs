using Entities.Models;
using MongoDB.Driver;

namespace DataAccess.Interfaces
{
    public class MessageRepo : GenericRepo<Message>, IMessageRepo
    {
        public MessageRepo(IMongoCollection<Message> collection) : base(collection)
            { }
    }
}
