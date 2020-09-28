using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_4_Cliente_Vuelos
{
    public class ClienteVuelos:INotifyPropertyChanged
    {
        public delegate void movimiento();
        public event movimiento AlHaberMovimiento;


        HttpClient cliente = new HttpClient();

        public event PropertyChangedEventHandler PropertyChanged;

        public ClienteVuelos()
        {
            cliente.BaseAddress = new Uri("http://vuelos.itesrc.net/");
        }

        public async void Agregar(DatosVuelo vuelo)
        {
            //if (vuelo == null)
            //    throw new ArgumentException("Ingrese los datos del vuelo");
            //if (string.IsNullOrEmpty(vuelo.Hora))
            //    throw new ArgumentException("Ingrese la Hora del vuelo");
            //if (string.IsNullOrEmpty(vuelo.Destino))
            //    throw new ArgumentException("Ingrese el nombre del destino");
            //if (string.IsNullOrEmpty(vuelo.Vuelo))
            //    throw new ArgumentException("Ingrese el numero de vuelo");
            //if (string.IsNullOrEmpty(vuelo.Estado))
            //    throw new ArgumentException("Seleccione un estado de vuelo");

            var json = JsonConvert.SerializeObject(vuelo);
            var resultado = await cliente.PostAsync("/Tablero", new StringContent(json, Encoding.UTF8, "application/json"));
            resultado.EnsureSuccessStatusCode();

            
        }

        public async void Editar(DatosVuelo vuelo)
        {
            var json = JsonConvert.SerializeObject(vuelo);
            var resultado = await cliente.PutAsync("/Tablero", new StringContent(json, Encoding.UTF8, "application/json"));
            resultado.EnsureSuccessStatusCode();

        }

        public async void Eliminar(DatosVuelo vuelo)
        {
            var json = JsonConvert.SerializeObject(vuelo);
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, "/Tablero");
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await cliente.SendAsync(message);
            resultado.EnsureSuccessStatusCode();

        }
        public IEnumerable<DatosVuelo> ListaVuelos { get; set; }
        public async void RegresarDatos()
        {
             
             IEnumerable<DatosVuelo> model = null;
           
            var client = new HttpClient();
            var response = await client.GetAsync("http://vuelos.itesrc.net/Tablero");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<IEnumerable<DatosVuelo>>(jsonString);
                ListaVuelos = model;
                AlHaberMovimiento?.Invoke();
            }
           

        }
  
    }
}
