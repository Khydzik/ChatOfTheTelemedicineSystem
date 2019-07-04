using GraduateWork.Client.Wpf.ViewModel;
using GraduateWork.Common.CommonProject.Models;
using System.Windows;

namespace GraduateWork.Client.Wpf.View
{
    /// <summary>
    /// Interaction logic for PageDoctorFromListBox.xaml
    /// </summary>
    public partial class PageDoctorFromListBox : Window
    {
        public PageDoctorFromListBox(UsersInfoModel doctorPageFromListBoxModel, string UserId)
        {
            InitializeComponent();
            DataContext = new DoctorPageFromListBoxViewModel(doctorPageFromListBoxModel, UserId);
        }
    }
}
