using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClienteAlumnos
{
    public class Cliente
    {
        HttpClient cliente = new HttpClient();
        public Cliente()
        {
            cliente.BaseAddress = new Uri("http://localhost:8080/");// Aqui es a donde se van estar enviando los datos
          
        }

        public async void Agregar(DatosAlumno a)
        {
            var json = JsonConvert.SerializeObject(a);
            var result = await cliente.PostAsync("practica4/Alumnos", new StringContent(json, Encoding.UTF8,"application/json"));
            result.EnsureSuccessStatusCode();//este metodo verifica que regrese un codigo de estado de 200 
           
        }
        public async void Editar(DatosAlumno a)
        {
            var json = JsonConvert.SerializeObject(a);
            var result = await cliente.PutAsync("practica4/Alumnos", new StringContent(json, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();

        }

        public async void Eliminar(DatosAlumno a)
        {
            var json = JsonConvert.SerializeObject(a);
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, "practica4/Alumnos");
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await cliente.SendAsync(message);
            result.EnsureSuccessStatusCode();

        }

    }
}
