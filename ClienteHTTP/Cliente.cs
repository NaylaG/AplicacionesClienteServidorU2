using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClienteHTTP
{
    public class Cliente
    {
        HttpClient cliente = new HttpClient();
        public Cliente()
        {
            cliente.BaseAddress = new Uri("http://localhost:8080/");
        }
        public async void IncrementarRojo()
        {
            var result = await cliente.PostAsync("/practica3", new StringContent("color=Rojo"));
            if(result.StatusCode!= HttpStatusCode.OK)
            {
                throw new Exception("No se realizo la oprecion");
            }
        }

        public async void IncrementarVerde()
        {
            var result = await cliente.PostAsync("/practica3", new StringContent("color=Verde"));
            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("No se realizo la oprecion");
            }

        }
    }
}
