using Fitnessstudio.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für KundenVerwaltung.xaml
    /// </summary>
    public partial class KundenVerwaltung : Page
    {
        public KundenVerwaltung()
        {
            InitializeComponent();
        }

        public void SetDataContext(object dataContext)
        {
            this.DataContext = dataContext;
        }
    }
}
