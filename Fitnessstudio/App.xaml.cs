using Fitnessstudio.Views;
using Serilog;
using System;
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
        public App()
        {
            // Konfiguriere Serilog beim Start der Anwendung
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Application starting up");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                MainWindow = new LoginView(CurrentUser);
                MainWindow.Show();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while starting the application");
                throw;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Log.Information("Application shutting down");
            Log.CloseAndFlush();
        }
    }
}
