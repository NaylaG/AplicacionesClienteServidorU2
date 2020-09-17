using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador
{
    public class Valores:INotifyPropertyChanged
    {
        private int verde;

        public int ValorVerde
        {
            get { return verde; }
            set { verde = value;
                OnPropetyChanged("ValorVerde");
            }
        }

        private int rojo;

        public int ValorRojo
        {
            get { return rojo; }
            set { rojo = value;
                OnPropetyChanged("ValorRojo");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropetyChanged(string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}
