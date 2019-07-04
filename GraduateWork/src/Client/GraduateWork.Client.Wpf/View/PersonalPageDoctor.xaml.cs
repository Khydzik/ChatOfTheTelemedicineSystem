using GraduateWork.Client.Wpf.ViewModel;
using GraduateWork.Common.CommonProject.Models;
using System.Windows;

namespace GraduateWork.Client.Wpf.View
{
    /// <summary>
    /// Interaction logic for PersonalPageDoctor.xaml
    /// </summary>
    public partial class PersonalPageDoctor : Window
    {
        public PersonalPageDoctor(UsersInfoModel patientInfoModel, CountMessageForPersonalPage countMessageForPersonalPage, Connected connected)
        {
            InitializeComponent();

            DataContext = new PersonalPageDoctorViewModel(patientInfoModel, countMessageForPersonalPage, connected);
        }
    }
}
