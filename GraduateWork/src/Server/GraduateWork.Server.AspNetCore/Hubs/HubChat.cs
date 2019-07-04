using GraduateWork.Common.CommonProject.Models;
using GraduateWork.Server.Core;
using GraduateWork.Server.Data;
using GraduateWork.Server.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduateWork.Server.AspNetCore.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> _connectionId = new Dictionary<string, string>();
        private readonly IMessageService _messageService;
        private static ChatHub _chatHub;

        public ChatHub() { }

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public static ChatHub GetInstance()
        {
            if (_chatHub == null)
                _chatHub = new ChatHub();
            return _chatHub;
        }

        public async Task SendMessage(ChatMessage chatMessage)
        {
            if (!_connectionId.TryGetValue(chatMessage.FirstUserId, out var connectionId))
                return;

            await _messageService.AddMessageAsync(chatMessage);

            await Clients.All.SendAsync("ReceiveMessage", chatMessage.Message, chatMessage.Name, chatMessage.SecondUserId);

                           
        }

        public async Task<Connected> OnConnectedAsync(string connectionID, string userID)
        {
            _connectionId.Add(userID, connectionID);
            return new Connected
            {
                ConnectedID = connectionID
            };
        }

        public override Task OnDisconnectedAsync(Exception stopCalled)
        {
           string userId = Context.User.Claims.ToString();

            _connectionId.Remove(userId);

            return base.OnDisconnectedAsync(stopCalled);
        }
    }
}
