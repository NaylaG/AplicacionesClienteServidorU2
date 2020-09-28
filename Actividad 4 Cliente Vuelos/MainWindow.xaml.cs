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

namespace Actividad_4_Cliente_Vuelos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = datos;
         
            cliente.RegresarDatos();
           
            cliente.AlHaberMovimiento += Cliente_AlHaberMovimiento;
            
        }

        private void Cliente_AlHaberMovimiento()
        {
            cliente.RegresarDatos();
            dtgVuelos.ItemsSource = cliente.ListaVuelos;
        }

      
        DatosVuelo datos = new DatosVuelo();
        ClienteVuelos cliente = new ClienteVuelos();

        
        private  void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente.Agregar(datos);
               
                txtDestino.Clear();
                txtVuelo.Clear();
                txtHora.Clear();
                cmbEstados.SelectedIndex = -1;
                cliente.RegresarDatos();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente.Eliminar(datos);
                cliente.RegresarDatos();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //dtgVuelos.SelectedItem
                cliente.Editar(datos);
                cliente.RegresarDatos();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private  void btnActualiza_Click(object sender, RoutedEventArgs e)
        {
            // cliente.RegresarDatos();
            //dtgVuelos.ItemsSource = cliente.ListaVuelos;
        }
    }
}
