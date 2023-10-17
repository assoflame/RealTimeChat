using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class RoomNotFoundException : NotFoundException
    {
        public RoomNotFoundException(string roomName)
            : base(@$"The room with name: '{roomName}' doesn't exist in the database")
        { }
    }
}
