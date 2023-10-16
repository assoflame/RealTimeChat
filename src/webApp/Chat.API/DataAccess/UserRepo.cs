using Entities;
using MongoDB.Driver;

namespace DataAccess.Interfaces
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(IMongoCollection<User> collection) : base(collection)
            { }
    }
}
