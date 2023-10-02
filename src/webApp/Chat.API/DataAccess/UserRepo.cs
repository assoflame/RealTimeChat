using Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(IMongoCollection<User> collection) : base(collection)
            { }
    }
}
