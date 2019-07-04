using GraduateWork.Client.Wpf.ViewModel;
using System.Windows;

namespace GraduateWork.Client.Wpf.View
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();

            DataContext = new AuthorizationViewModel();
        }
    }
}
