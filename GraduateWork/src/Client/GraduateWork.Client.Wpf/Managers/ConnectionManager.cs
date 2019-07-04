using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using GraduateWork.Common.CommonProject;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Headers;
using GraduateWork.Common.CommonProject.Models;
using GraduateWork.Client.Wpf.FileAuthorize;
using System.Net;
using GraduateWork.Client.Wpf.ViewModel;

namespace GraduateWork.Client.Wpf.Managers
{
    public class ConnectionManager
    {
        private static string Uri = "http://localhost:5000";
        private HubConnection _hubConnection;

        public delegate void Dispatcher(ChatMessage chat);
        public event Dispatcher DisplayMessage;
        private static ChatMessage _newChatMessage;
        private string Token { get; set; }
        private string _token { get; set; }
        private string _message { get; set; }
        private string authorMessage { get; set; }
        private string _secondUserId { get; set; }
        private string _userName { get; set; }
        private static ConnectionManager _instance;
        private static ChatPageViewModel _chatPageViewModel;
        private static OperationFile _operationFile;

        public ConnectionManager()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(Uri + "/chatHub").Build();
            _hubConnection.ServerTimeout = TimeSpan.FromMilliseconds(100000);
            _operationFile = OperationFile.GetInstance();
            _chatPageViewModel = new ChatPageViewModel();
            _newChatMessage = new ChatMessage();
        }

        public static ConnectionManager GetInstance()
        {
            if (_instance == null)
                _instance = new ConnectionManager();
            return _instance;
        }

        public async Task ConnectionHub()
        {
            
            ServicePointManager.DefaultConnectionLimit = 10;
            await _hubConnection.StartAsync();
        }

        public async Task SendMessage(ChatMessage chatMessage)
        {            
            try
            {
                await _hubConnection.InvokeAsync("SendMessage", chatMessage);
              
                _hubConnection.On<string,string,string>("ReceiveMessage", (_message, _username, _secondUserId) =>
                {
                    _newChatMessage.Message = _message;
                    _newChatMessage.Name = _username;
                   _newChatMessage.SecondUserId = _secondUserId;
                         
                });

                DisplayMessage += _chatPageViewModel.DisplayMessageInChat;
                DisplayMessage?.Invoke(_newChatMessage);                
               }
            catch (Exception ex)
            {
                return ;
            }
        }

        
        public async Task<Connected> GetConnectionId()
        {
            try
            {
                _token = await _operationFile.ReadTokenFromFile();

                using (var client = new HttpClient())
                {
                  
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    var response = await client.GetAsync(Uri + "/api/v1/Connected").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<Connected>(await response.Content.ReadAsStringAsync());

                    else { return null; }
                }
            }
            catch (Exception)
            { return null; }
        }

        public async Task<SavedChats> SavedChats(string userId)
        {
            try
            {
                _token = await _operationFile.ReadTokenFromFile();

                var content = new StringContent(JsonConvert.SerializeObject(new SavedChats
                {
                    UserId = new Guid(userId)
                }));

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync(Uri + "/api/v1/HistoryChats", content);
                    if (response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<SavedChats>(await response.Content.ReadAsStringAsync());

                    else { return null; }
                }
            }
            catch (Exception) { return null; }
        }

        public async Task<CountMessageForPersonalPage> CountMessages(string id)
        {
            try
            {
                _token = await _operationFile.ReadTokenFromFile();

                var content = new StringContent(JsonConvert.SerializeObject(new CountMessageForPersonalPage
                {
                    UserId = new Guid(id)
                }));

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync(Uri + "/api/v1/CountMessages", content);
                    if (response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<CountMessageForPersonalPage>(await response.Content.ReadAsStringAsync());

                    else { return null; }
                }
            }
            catch (Exception)
            { return null; }
        }




        public async Task<HttpResponseMessage> UpdateUser(UsersInfoModel usersInfoModel)
        {
            try
            {
                _token = await _operationFile.ReadTokenFromFile();
                var content = new StringContent(JsonConvert.SerializeObject(usersInfoModel));
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    var response = await client.PutAsync(Uri + "/api/v1/Update", content);
                    return response;
                }
            }
            catch (Exception)
            { return null; }
        }


        public async Task<UsersInfoModel> GetUserInfoAsync(UsersInfoModel _patient)
        {
            try
            {
                _token = await _operationFile.ReadTokenFromFile();
                var content = new StringContent(JsonConvert.SerializeObject(_patient));
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    var response = await client.PostAsync(Uri + "/api/v1/InfoAboutUsers", content);
                    if (response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<UsersInfoModel>(await response.Content.ReadAsStringAsync());

                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> Login(UserLoginModel values)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(values));
                using (var client = new HttpClient())
                {
                    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    var response = await client.PostAsync(Uri + "/api/v1/Authorization", content);
                    return response;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> RegisterUser(RegistrationUserModel values)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(values));

                using (var client = new HttpClient())
                {
                    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    var result = await client.PostAsync(Uri + "/api/v1/Registration", content);
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
