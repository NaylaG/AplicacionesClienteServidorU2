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

namespace ClienteHTTP
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cliente c = new Cliente();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRojo_Click(object sender, RoutedEventArgs e)
        {
            c.IncrementarRojo();
        }

        private void btnVerde_Click(object sender, RoutedEventArgs e)
        {
            c.IncrementarVerde();
        }
    }
}
