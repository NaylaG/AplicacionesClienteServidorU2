using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace Practica2
{
    public class Servidor: INotifyPropertyChanged
    {
        HttpListener listener;
       public string Mensaje { get; set; }     
        public string Color { get; set; }
    

        Dispatcher dispatcher;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropetyChanged (string mensaje, string color)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(mensaje));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(color));
        }

        public Servidor()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            listener = new HttpListener();

            listener.Prefixes.Add("http://*:80/practica2/");
            listener.Start();
            listener.BeginGetContext(OnRequest, null);
        }

        private void OnRequest(IAsyncResult ar)
        {
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(OnRequest, null);

            if(context.Request.Url.LocalPath=="/practica2/" || context.Request.Url.LocalPath=="/practica2")
            {
                var buffer = File.ReadAllBytes("index.html");
                context.Response.ContentType = "text/html";
                context.Response.StatusCode = 200;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
            }
            else if(context.Request.Url.LocalPath=="/practica2/mensaje" && context.Request.HttpMethod=="GET")
            {
                if(context.Request.QueryString["color"]!="" && context.Request.QueryString["mensaje"]!="")
                {
                    var color = context.Request.QueryString["color"];
                    var mensaje = context.Request.QueryString["mensaje"];
                    Cambiar(color, mensaje);
                    context.Response.StatusCode = 200;
                    context.Response.Redirect("/practica2/");
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.StatusDescription = "Olvidaste escribir el mensaje";
                }
            }
            else
            {
                context.Response.StatusCode = 404;

            }
            context.Response.Close();

        }


        private void Cambiar (string color, string mensaje)
        {

            this.dispatcher.Invoke(() => Mensaje = mensaje) ;
            this.dispatcher.Invoke(() => Color = color);
            OnPropetyChanged("Mensaje", "Color");

        }
    }
}
