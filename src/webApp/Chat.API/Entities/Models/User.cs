﻿namespace Entities.Models
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; }
        public string PasswordHash { get; set; }
    }
}
