using GraduateWork.Client.Wpf.ViewModel;
using GraduateWork.Common.CommonProject.Models;
using System.Windows;

namespace GraduateWork.Client.Wpf.View
{
    /// <summary>
    /// Interaction logic for PagePatientFromListBox.xaml
    /// </summary>
    public partial class PagePatientFromListBox : Window
    {
        public PagePatientFromListBox(UsersInfoModel userInfoModel, string UserId)
        {
            InitializeComponent();
            DataContext = new PatientPageFromListBoxViewModel(userInfoModel, UserId);
        }
    }
}
