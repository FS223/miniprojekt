using Fitnessstudio.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Fitnessstudio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Account CurrentUser { get; set; }
        public App() { }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new LoginView(CurrentUser) {
                //DataContext = new MainWindowViewModel()
            };
            MainWindow.Show();

            base.OnStartup(e);

        }
    }

}
