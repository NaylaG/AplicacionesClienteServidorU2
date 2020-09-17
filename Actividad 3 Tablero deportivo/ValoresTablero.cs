using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Actividad_3_Tablero_deportivo
{
    public class ValoresTablero:INotifyPropertyChanged
    {
        private string nombreEquipo1;
        public string NombreEquipo1
        {
            get { return nombreEquipo1; }
            set { nombreEquipo1 = value;
                OnPropertyChanged("NombreEquipo1");
            }
        }


        private string nombreEquipo2;
        public string NombreEquipo2
        {
            get { return nombreEquipo2; }
            set { nombreEquipo2 = value;
                OnPropertyChanged("NombreEquipo2");
            }
        }


        private int golesEquipo1;
        public int GolesEquipo1
        {
            get { return golesEquipo1; }
            set { golesEquipo1 = value;
                OnPropertyChanged("GolesEquipo1");
            }
        }


        private int golesEquipo2;
        public int GolesEquipo2
        {
            get { return golesEquipo2; }
            set { golesEquipo2 = value;
                OnPropertyChanged("GolesEquipo2");
            }
        }


        private string tiempo;

        public string Tiempo
        {
            get { return tiempo; }
            set { tiempo = value;
                OnPropertyChanged("Tiempo");
            }
        }

        private string cronometro;

        public string Cronometro
        {
            get { return cronometro; }
            set { cronometro = value;
                OnPropertyChanged("Cronometro");
            }
        }
            


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged (string propiedad) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

    }
}
