using GraduateWork.Client.Wpf.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GraduateWork.Client.Wpf.View
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        public RegistrationView()
        {
            InitializeComponent();

            DataContext = new RegistrationViewModel();
        }
    }
}
