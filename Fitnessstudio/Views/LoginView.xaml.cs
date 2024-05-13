using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Fitnessstudio;

namespace Fitnessstudio.Views
{
    /// <summary>
    /// Interaktionslogik für LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private Auth auth;
        public LoginView()
        {
            InitializeComponent();
            auth = new Auth();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Password))
            {
                return;
            }

            bool isAuthenticated = await auth.HandleLoginAsync(txtUser.Text, txtPass.Password);

            if (isAuthenticated)
            {
                // Proceed with successful login
                lblError.Content = "";
                lblError.Visibility = Visibility.Hidden;
                MessageBox.Show("Login successful!");
                Dashboard dashboard = new Dashboard();
                this.Visibility = Visibility.Hidden;
                dashboard.WindowState = WindowState.Normal;
                dashboard.Owner = this;
                dashboard.Show();
            }
            else
            {
                // Handle unsuccessful login
                lblError.Content = "Invalid username or password!";
                lblError.Visibility = Visibility.Visible;
            }
        }
    }
}
