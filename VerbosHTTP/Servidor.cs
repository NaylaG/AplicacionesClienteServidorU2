using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace VerbosHTTP
{
    public class Servidor
    {
        HttpListener Listener;

        public ObservableCollection<string> Nombres { get; set; } = new ObservableCollection<string>();
        Dispatcher dispatcher;
        public Servidor()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            Listener = new HttpListener();
            //existen 65536 puertos
            Listener.Prefixes.Add("http://*:80/practica2/");
            Listener.Start();
            Listener.BeginGetContext(OnRequest, null);
        }

        private void OnRequest(IAsyncResult ar)
        {
            var context = Listener.EndGetContext(ar);
            Listener.BeginGetContext(OnRequest, null);

            if(context.Request.Url.LocalPath=="/practica2/" || context.Request.Url.LocalPath=="/practica2")
            {
                var buffer = File.ReadAllBytes("index.html");
                context.Response.ContentType = "text/html";
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
               
                context.Response.StatusCode = 200;
            }
            else if(context.Request.Url.LocalPath=="/practica2/recibirnombreget" && context.Request.HttpMethod=="GET")
            {
                if(context.Request.QueryString["nombre"]!=null)
                {
                    var nombre = context.Request.QueryString["nombre"];
                    Agregar(nombre);
                    context.Response.StatusCode = 200;
                    context.Response.Redirect("/practica2/");
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.StatusDescription = "Olvidaste incluir el nombre";
                }
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            context.Response.Close();
        }

        private void Agregar (string nombre)
        {
            dispatcher.BeginInvoke(new Action(() => { Nombres.Add(nombre); }));
                
               
            
        }
    }


}
