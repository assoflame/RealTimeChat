using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Message : BaseEntity
    {
        public string SenderName { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
