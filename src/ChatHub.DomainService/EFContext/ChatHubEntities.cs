﻿using ChatHub.Domain.MessageRooms;
using ChatHub.Domain.Messages;
using ChatHub.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ChatHub.DomainService.EFContext
{
    public sealed class ChatHubEntities : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Connection String");
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageRoom> MessageRooms { get; set; }
    }
}