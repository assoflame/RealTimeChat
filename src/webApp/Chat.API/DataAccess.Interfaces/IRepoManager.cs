using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepoManager
    {
        IMessageRepo Messages { get; }
        IUserRepo Users { get; }
        IRoomRepo Rooms { get; }
    }
}
