using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string username)
            : base(@$"The user with username: '{username}' doesn't exist in the database")
        { }
    }
}
