using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Actividad_4_Cliente_Vuelos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int time = 0;
        private DispatcherTimer Timer;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = datos;
            
            cliente.AlHaberMovimiento += Cliente_AlHaberMovimiento;
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 5);
            Timer.Tick += Timer_Tick; ;
            Timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time >= 0)
            {

                cliente.RegresarDatos();
                time++;
            }
        }
       
        private void Cliente_AlHaberMovimiento()
        {
            
            dtgVuelos.ItemsSource = cliente.ListaVuelos;

        }

        
        DatosVuelo datos = new DatosVuelo();
        ClienteVuelos cliente = new ClienteVuelos();

        
        private  void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbEstados.SelectedIndex != -1)
                {
                    cliente.Agregar(datos);

                    txtDestino.Clear();
                    txtVuelo.Clear();
                    txtHora.Clear();
                    cmbEstados.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Seleccione un estado","Advertencia",MessageBoxButton.OK,MessageBoxImage.Warning);
                }
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

               if(dtgVuelos.SelectedIndex!=-1)
                {
                    DatosVuelo veliminar = new DatosVuelo();
                    veliminar = dtgVuelos.SelectedItem as DatosVuelo;
                    if (MessageBox.Show($"Estas seguro que quieres eliminar el vuelo de las {veliminar.Hora} hrs?", "Atención", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        cliente.Eliminar(veliminar);
                    }
                }
                else
                {
                    MessageBox.Show("Debes selecionar un vuelo", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
              

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
                if (dtgVuelos.SelectedIndex != -1)
                {
                    EditarVueloView vn = new EditarVueloView();
                    DatosVuelo vuelo = dtgVuelos.SelectedItem as DatosVuelo;
                    vn.DataContext = vuelo;
                    vn.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Debe seleccionar un vuelo", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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
