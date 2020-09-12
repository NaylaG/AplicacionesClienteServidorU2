using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServidorSimple
{
    public class Servidor
    {
        HttpListener server;

        public Servidor()
        {
            server = new HttpListener();
            server.Prefixes.Add("http://localhost/practica1/");
        }

        public void Iniciar()
        {
            server.Start();
            server.BeginGetContext(ResponderSolicitudes, null);
        }
        public void Detener()
        {
            server.Stop();
        }
        private void ResponderSolicitudes(IAsyncResult ar)
        {
            var context = server.EndGetContext(ar);
            server.BeginGetContext(ResponderSolicitudes, null);
            var peticion = context.Request;

            if(peticion.Url.LocalPath== "/practica1")
            {
                context.Response.StatusCode = 200;
                string respuesta = "<h1>DESARROLLO DE APLICACIONES CLIENTE/SERVIDOR</h1> <p>Nayla Garcia Mireles</p>";
                context.Response.ContentType = "text/html";
                byte[] buffer = Encoding.UTF8.GetBytes(respuesta);
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
            }
            //FECHA           
            else if(peticion.Url.LocalPath== "/practica1/fecha")
            {
            
                context.Response.StatusCode = 200;
               
                var fecha = DateTime.Now.ToString();
               
                byte[] buffer = Encoding.UTF8.GetBytes(fecha);

                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
            }
            //SUMA DOS VALORES
            else if(peticion.Url.LocalPath =="/practica1/suma" 
                && peticion.QueryString["num1"]!=null 
                && peticion.QueryString["num2"]!=null)
            {
                var num1 = int.Parse(peticion.QueryString["num1"]);
                var num2 = int.Parse(peticion.QueryString["num2"]);         
                context.Response.StatusCode = 200;
                int resultado = num1 + num2;
                byte[] buffer = Encoding.UTF8.GetBytes(resultado.ToString());
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            context.Response.Close();

            //if(context.Request.Url.LocalPath == "/practica1/")
            //{
            //    context.Response.StatusCode = 200;
            //    string respuesta = "<h1>Servidor Iniciado</h1><p>Iniciado</p>";
            //    byte[] buffer = Encoding.UTF8.GetBytes(respuesta);
            //    context.Response.ContentType = "text/html";
            //    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            //    context.Response.OutputStream.Close();
            //}
            //else if (context.Request.Url.LocalPath == "/practica1/saludame" 
            //    && context.Request.QueryString["nombre"]!=null)
            //{
            //    var nombre = context.Request.QueryString["nombre"];
            //    context.Response.StatusCode = 200;
            //    string respuesta = $"Hola {nombre}, Como estas?";
            //    byte[] buffer = Encoding.UTF8.GetBytes(respuesta);

            //    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            //    context.Response.OutputStream.Close();
            //}
            //else
            //{
            //    context.Response.StatusCode = 404;
            //}
        }

    }
}



