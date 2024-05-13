using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fitnessstudio.Views.Pages
{
    /// <summary>
    /// Interaktionslogik für KundenAdmin.xaml
    /// </summary>
    public partial class KundenAdmin : Page
    {
        public ObservableCollection<Person> Items { get; set; }
        public KundenAdmin()
        {
            InitializeComponent();
            Items = new ObservableCollection<Person>();
            GetPersonen();            
            DataContext = this;
        }


        private async void GetPersonen()
        {
            var databaseService = new DatabaseService();
            List<Person> Personen = await databaseService.GetPersonen();
            foreach (Person person in Personen)
            {
                Items.Add(person);
            }
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {

        }

        private void Button_NeuerKunde(object sender, RoutedEventArgs e)
        {

        }

    }
}

