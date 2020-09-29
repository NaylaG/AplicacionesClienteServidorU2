using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Actividad_4_Cliente_Vuelos
{
    public class ClienteVuelos
    {
        public delegate void movimiento();
        public event movimiento AlHaberMovimiento;
        public ObservableCollection<DatosVuelo> ListaVuelos { get; set; } = new ObservableCollection<DatosVuelo>();


        HttpClient cliente = new HttpClient();


        public ClienteVuelos()
        {
            cliente.BaseAddress = new Uri("http://vuelos.itesrc.net/");         
          
        }
      

        public async void Agregar(DatosVuelo vuelo)
        {

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
        
        
        public async void RegresarDatos()
        {
             
             ObservableCollection<DatosVuelo> model = null;
           
            var client = new HttpClient();
            var response = await client.GetAsync("http://vuelos.itesrc.net/Tablero");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<ObservableCollection<DatosVuelo>>(jsonString);
                ListaVuelos = model;
                AlHaberMovimiento?.Invoke();
            }


        }
       


    }
}
