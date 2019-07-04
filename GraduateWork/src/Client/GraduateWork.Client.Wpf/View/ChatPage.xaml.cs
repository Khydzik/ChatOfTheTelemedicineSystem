using GraduateWork.Client.Wpf.ViewModel;
using GraduateWork.Common.CommonProject.Models;
using System.Windows;

namespace GraduateWork.Client.Wpf.View
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Window
    {
        public ChatPage(SavedChats savedChats, UsersInfoModel usersInfoModel)
        {
            InitializeComponent();            

            DataContext = new ChatPageViewModel(savedChats, usersInfoModel);
        }
    }
}
