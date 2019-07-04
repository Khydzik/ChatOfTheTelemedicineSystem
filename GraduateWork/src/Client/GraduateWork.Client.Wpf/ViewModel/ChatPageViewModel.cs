using GraduateWork.Client.Wpf.Managers;
using GraduateWork.Common.CommonProject.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduateWork.Client.Wpf.ViewModel
{
    public class ChatPageViewModel: BindableBase
    {
        private string WriteManId { get; set; }
        public DelegateCommand<PrivateChat> SelectMessageCommand { get; }
        public DelegateCommand SendNewMessage { get; set; }
        private string _message;
        private string _name;
        private Guid UserID { get; set; }
        private ObservableCollection<PrivateChat> _chats;
        private readonly ConnectionManager _connectionManager;
        private PrivateChat _currentChat;

        public string Name
        {
            get { return this._name; }
            set { SetProperty(ref _name, value); }
        }

        public string WriteMessage
        {
            get { return this._message; }
            set { SetProperty(ref _message, value); }
        }

        public ObservableCollection<PrivateChat> ChatList
        {
            get { return _chats; }
            set { SetProperty(ref _chats, value); }
        }

        public PrivateChat CurrentChat { get { return _currentChat; } set { SetProperty(ref _currentChat, value); } }

        public ChatPageViewModel() { }

        public ChatPageViewModel(SavedChats savedChats, UsersInfoModel usersInfoModel)
        {
            SelectMessageCommand = new DelegateCommand<PrivateChat>(SelectChat);
            SendNewMessage = new DelegateCommand(SendMessage);
           
            Name = usersInfoModel.FullName;
            _connectionManager = new ConnectionManager();
            WriteManId = usersInfoModel.Id;
            InitializedChatsPage(savedChats);
            UserID = savedChats.UserId;
            CurrentChat = savedChats.SavedPrivateChats.SingleOrDefault(chat => chat.UserIdToWrite.ToString() == usersInfoModel.Id);
            CurrentChat.Messages = new ObservableCollection<ChatMessage>();
        }

        public void InitializedChatsPage(SavedChats savedChats)
        {
            try
            {
                if (savedChats.SavedPrivateChats != null)                  
                        ChatList = new ObservableCollection<PrivateChat>(savedChats.SavedPrivateChats);


                ChatList.Add(new PrivateChat { Name = "Олександр Журба" });
                ChatList.Add(new PrivateChat { Name = "Fedir Khydzik" });
                ChatList.Add(new PrivateChat { Name = "Olya Ivanenko" });
                ChatList.Add(new PrivateChat { Name = "Віталій Пилипенко" });
                ChatList.Add(new PrivateChat { Name = "Іван Пилипенко" });
                ChatList.Add(new PrivateChat { Name = "Volodya Ivan" });

            }
            catch (Exception) { return; }
        }

        public async void SendMessage()
        {
            
            ChatMessage msg = new ChatMessage
            {
                ChatID = CurrentChat.Id,
                Name = this.Name,
                FirstUserId = WriteManId,
                SecondUserId = UserID.ToString(),
                Message = WriteMessage,
                DateTime = DateTime.Now
            };

           

            await _connectionManager.ConnectionHub();

            await _connectionManager.SendMessage(msg);


            await _connectionManager.DisplayMessage = DisplayMessageInChat;
            var result = await _connectionManager.SendMessage(msg);
            DisplayMessage(result);
        }

        public void DisplayMessageInChat(ChatMessage result)
        {
            if (result.SecondUserId == UserID.ToString())
                result.Name = null;

            WriteMessage = string.Empty;
           

            CurrentChat?.Messages?.Add(new ChatMessage { Message = result.Message, Name = result.Name });
        }

        public void SelectChat(PrivateChat chat)
        {
            CurrentChat = chat;
            Name = chat.Name;
            WriteManId = chat.Id.ToString();
        }
    }
}
