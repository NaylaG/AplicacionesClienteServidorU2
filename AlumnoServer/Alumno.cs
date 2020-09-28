using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlumnoServer
{
    public class Alumno:INotifyPropertyChanged
    {
        private string numControl;

        public string NumeroControl
        {
            get { return numControl; }
            set { numControl = value;
                AlHaberCambio("NumeroControl");
            }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value;
                AlHaberCambio("Nombre");

            }
        }

        private string carrera;

        public string Carrera
        {
            get { return carrera; }
            set { carrera = value;
                AlHaberCambio("Carrera");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void AlHaberCambio (string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}
