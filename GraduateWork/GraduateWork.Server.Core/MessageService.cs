using System;
using System.Threading.Tasks;
using GraduateWork.Common.CommonProject.Models;
using GraduateWork.Server.Data;
using GraduateWork.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduateWork.Server.Core
{
    public class MessageService : IMessageService
    { 

        private readonly IRepository<PrivateMessageEntity> _messageEntityRepository;

        public MessageService(IRepository<PrivateMessageEntity> messageEntityRepository)
        {
            _messageEntityRepository = messageEntityRepository;
        }

        public async Task AddMessageAsync(ChatMessage chatMessage)
        {
            var chat = await _messageEntityRepository.Query().FirstOrDefaultAsync(r => r.PrivatChatId == chatMessage.ChatID);

            if(chat.Equals(null))
            {
                var newMessage = new PrivateMessageEntity
                {
                    PrivatChatId = new Guid(),
                    Content = chatMessage.Message,
                    PrivateChat = new PrivateChatEntity
                    {
                        FirstUserInfoId = new Guid(chatMessage.FirstUserId),
                        SecondUserInfoId = new Guid(chatMessage.SecondUserId)
                    },
                };
                await _messageEntityRepository.InsertAsync(newMessage);
            }

            chat.Content = chatMessage.Message;

            await _messageEntityRepository.InsertAsync(chat);
        }
    }
}
