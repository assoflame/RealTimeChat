﻿using DataAccess.Interfaces;
using Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RepoManager : IRepoManager
    {
        private readonly Lazy<IMessageRepo> _messages;
        private readonly Lazy<IUserRepo> _users;
        private readonly Lazy<IRoomRepo> _rooms;

        public RepoManager(IMongoDatabase db)
        {
            _messages = new Lazy<IMessageRepo>(() => new MessageRepo(db.GetCollection<Message>("messages")));
            _users = new Lazy<IUserRepo>(() => new UserRepo(db.GetCollection<User>("users")));
            _rooms = new Lazy<IRoomRepo>(() => new RoomRepo(db.GetCollection<Room>("rooms")));
        }

        public IMessageRepo Messages => _messages.Value;
        public IUserRepo Users => _users.Value;
        public IRoomRepo Rooms => _rooms.Value;
    }
}