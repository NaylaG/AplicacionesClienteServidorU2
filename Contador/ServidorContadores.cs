using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Threading;

namespace Contador
{
    public class ServidorContadores
    {
        HttpListener listener;
        public Valores Valores { get; set; } = new Valores();

        Dispatcher dispatcher;
        public ServidorContadores()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:8080/practica3/");
            listener.Start();

            dispatcher = Dispatcher.CurrentDispatcher;

            listener.BeginGetContext(Recibir, null);

        }

        private void Recibir(IAsyncResult ar)
        {
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(Recibir, null); //para que espere por otra peticion

            //para quitar la diagonal
            var url = context.Request.Url.LocalPath;
            if(url.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1, 1);
            }

            if(url=="/practica3" && context.Request.HttpMethod=="GET")
            {
                var index = System.IO.File.ReadAllBytes("index.html");
                context.Response.ContentType = "text/html";
                context.Response.OutputStream.Write(index, 0, index.Length);
                context.Response.StatusCode = 200;
                context.Response.OutputStream.Close();
            }
            if (url == "/practica3" && context.Request.HttpMethod == "POST")
            {
                StreamReader stream = new StreamReader(context.Request.InputStream);
                string variables=stream.ReadToEnd();
                var datos = HttpUtility.ParseQueryString(variables);

                Incrementar(datos["color"]);
                context.Response.StatusCode = 200;
                context.Response.Redirect("/practica3");
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            context.Response.Close();
        }

        public void Incrementar(string color)
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
                if (color == "Rojo")
                {
                    Valores.ValorRojo++;
                }
                else
                {
                    Valores.ValorVerde++;
                }
            }
            ));
        }
    }
}
