using GraduateWork.Client.Wpf.ViewModel;
using GraduateWork.Common.CommonProject.Models;
using System.Windows;

namespace GraduateWork.Client.Wpf.View
{
    /// <summary>
    /// Interaction logic for PersonalPagePatient.xaml
    /// </summary>
    public partial class PersonalPagePatient : Window
    {
        public PersonalPagePatient(UsersInfoModel patientInfoModel, CountMessageForPersonalPage countMessageForPersonalPage, Connected connected)
        {
            InitializeComponent();

            DataContext = new PersonalPagePatientViewModel(patientInfoModel, countMessageForPersonalPage, connected);
        }
    }
}
