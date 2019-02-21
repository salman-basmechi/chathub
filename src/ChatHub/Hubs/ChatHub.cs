﻿using ChatHub.Data.EFContext;
using ChatHub.Models.MessageRooms;
using ChatHub.Models.Messages;
using ChatHub.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatHub.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ChatHubEntities dbContext;

        public ChatHub(ChatHubEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SendMessage(string text, Guid messageRoomId)
        {
            var message = dbContext.Messages.Add(new Message()
            {
                UserId = Context.User.GetId(),
                MessageRoomId = messageRoomId,
                SubmitDateTime = DateTime.Now,
                Text = text
            }).Entity;

            await dbContext.SaveChangesAsync();

            await Clients.All.SendAsync("OnReceivedMessage", Context.User.GetName(), message);
        }

        public async Task CreateMessageRoom(string name)
        {
            var messageRoom = dbContext.MessageRooms.Add(new MessageRoom()
            {
                Name = name,
                SubmitDateTime = DateTime.Now
            });

            await Clients.All.SendAsync("OnMessageRoomCreate", messageRoom);
        }
    }
}