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

namespace ClienteAlumnos
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
        }
        DatosAlumno datos = new DatosAlumno();
        Cliente cliente = new Cliente();
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente.Agregar(datos);
            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente.Eliminar(datos);
            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente.Editar(datos);
            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }
        }
    }
}
