using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AlumnoServer
{
    public class ServidorAlumnos
    {
        public CatalogoAlumnos Alumnos { get; set; } = new CatalogoAlumnos();
        HttpListener server;
        Dispatcher current;

        public ServidorAlumnos()
        {
            current = Dispatcher.CurrentDispatcher;//ref hilo actual
            server = new HttpListener();
            server.Prefixes.Add("http://*:8080/practica4/");
         
            server.Start();
            server.BeginGetContext(OnContext, null);


        }

        private void OnContext(IAsyncResult ar)
        {
            var context = server.EndGetContext(ar);
            server.BeginGetContext(OnContext, null);

            if(context.Request.Url.LocalPath=="/practica4/Alumnos")          
            {
                //cuando queremos que nos regrese la info
                if (context.Request.HttpMethod == "GET")
                {
                    var datos = JsonConvert.SerializeObject(Alumnos.Alumnos);
                    byte[] buffer = Encoding.UTF8.GetBytes(datos);
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.StatusCode = 200;

                }
                else
                {
                    //validar que el alumno viene en formato jason y no viene vacio
                    if (context.Request.ContentType.StartsWith("application/json" )&& context.Request.ContentLength64 > 0)
                    {
                        StreamReader reader = new StreamReader(context.Request.InputStream);
                        var datos = reader.ReadToEnd();//ALUMNO EN JSON

                        var alumno = JsonConvert.DeserializeObject<Alumno>(datos);

                        //DISPATCHER
                        //Aqui yo le envio la informacion
                        current.Invoke(new Action(() =>
                        {
                            if (context.Request.HttpMethod == "POST")//INSERTAR
                            {
                                Alumnos.Agregar(alumno);
                            }
                            else if (context.Request.HttpMethod == "PUT")
                            {
                                Alumnos.Editar(alumno);
                            }
                            else if (context.Request.HttpMethod == "DELETE")
                            {
                                Alumnos.Eliminar(alumno);
                            }
                        }
                        ));

                        context.Response.StatusCode = 200;
                    }
                    else
                    {
                        context.Response.StatusCode = 400;

                    }
                }

            }
            else
            {
                context.Response.StatusCode = 404;
          }  
           context.Response.Close();

        }
    }
}
