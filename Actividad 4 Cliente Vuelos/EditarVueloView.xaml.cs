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
using System.Windows.Shapes;

namespace Actividad_4_Cliente_Vuelos
{
    /// <summary>
    /// Lógica de interacción para EditarVueloView.xaml
    /// </summary>
    public partial class EditarVueloView : Window
    {
       
        public EditarVueloView()
        {
            InitializeComponent();
            txtHora.IsEnabled = false;
            txtVuelo.IsEnabled = false;
        }

        ClienteVuelos cliente = new ClienteVuelos();
       
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            DatosVuelo vuelo = this.DataContext as DatosVuelo;
            try
            {
                
                cliente.Editar(vuelo);
                this.Close();
            }
            catch (Exception n)
            {

                MessageBox.Show(n.Message);
            }
        }
    }
}
