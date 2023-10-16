﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public IList<string> Admins { get; set; }
        public IList<string> BlackList { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
