using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Web;

namespace Actividad_3_Tablero_deportivo
{
    public class ServidorTablero
    {
        HttpListener listener;
        public ValoresTablero Valores = new ValoresTablero();
        Dispatcher dispatcher;

        private int time = 0;
        private DispatcherTimer Timer;
       
        public ServidorTablero()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:9898/actividad3/");
            listener.Start();
            dispatcher = Dispatcher.CurrentDispatcher;
            listener.BeginGetContext(Recibir, null);
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Segundos;
            

        }
        
        public void Segundos (object sender, EventArgs e)
        {
            if (time >= 0)
            {



                if (time < 599)
                {
                    if (time % 60 == 59)
                    {
                        time++;
                        Valores.Cronometro = string.Format("0{0}:0{0}", time / 60, time % 60);
                    }

                    else if (time % 60 > 8)
                    {
                        time++;
                        Valores.Cronometro = string.Format("0{0}:{1}", time / 60, time % 60);
                    }
                    else if (time % 60 <= 8)
                    {
                        time++;
                        Valores.Cronometro = string.Format("0{0}:0{1}", time / 60, time % 60);
                    }
                }
                else
                {
                    if (time % 60 == 59)
                    {
                        time++;
                        Valores.Cronometro = string.Format("{0}:0{0}", time / 60, time % 60);
                    }

                    else if (time % 60 > 8)
                    {
                        time++;
                        Valores.Cronometro = string.Format("{0}:{1}", time / 60, time % 60);
                    }
                    else if (time % 60 <= 8)
                    {
                        time++;
                        Valores.Cronometro = string.Format("{0}:0{1}", time / 60, time % 60);
                    }
                }




            }
        }

        
        private void Recibir(IAsyncResult ar)
        {
            var contexto = listener.EndGetContext(ar);
            listener.BeginGetContext(Recibir, null);


            var url = contexto.Request.Url.LocalPath;
            if(url.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1, 1);
            }


            if (url == "/actividad3" && contexto.Request.HttpMethod=="GET")
            {
                var index = System.IO.File.ReadAllBytes("index.html");
                contexto.Response.ContentType = "text/html";
                contexto.Response.OutputStream.Write(index, 0, index.Length);
                contexto.Response.StatusCode = 200;
                contexto.Response.OutputStream.Close();

            }
            else if (url == "/actividad3" && contexto.Request.HttpMethod == "POST")
            {
                StreamReader stream = new StreamReader(contexto.Request.InputStream);
                string variables = stream.ReadToEnd();
                var datos = HttpUtility.ParseQueryString(variables);

                if(datos["nombre1"]!="" && datos["nombre2"] != "" && datos["tiempo"] != "")
                {
                    AgregarDatos(datos["nombre1"], datos["nombre2"], datos["tiempo"]);
                    contexto.Response.StatusCode = 200;
                    contexto.Response.Redirect("/actividad3/marcador");
                }
                else
                {
                    contexto.Response.StatusCode = 400;
                }
               
               
            }
            else if (url == "/actividad3/marcador" && contexto.Request.HttpMethod == "GET")
            {
                var marcador = System.IO.File.ReadAllBytes("marcador.html");
                contexto.Response.ContentType = "text/html";
                contexto.Response.OutputStream.Write(marcador, 0, marcador.Length);
                contexto.Response.StatusCode = 200;
                contexto.Response.OutputStream.Close();
            }
            else if(url== "/actividad3/marcador" && contexto.Request.HttpMethod=="POST")
            {
                StreamReader stream = new StreamReader(contexto.Request.InputStream);
                string variables = stream.ReadToEnd();
                var datos = HttpUtility.ParseQueryString(variables);

                AgregarGol(datos["goles"]);
                TiempoPartido(datos["detener"]);
                contexto.Response.StatusCode = 200;
                contexto.Response.Redirect("/actividad3/marcador");

            }
            else
            {
                contexto.Response.StatusCode = 404;
            }

            contexto.Response.Close();
        }


        public void AgregarGol (string anotacion)
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
                if (anotacion == "Equipo 1")
                    Valores.GolesEquipo1++;
                else if(anotacion=="Equipo 2")
                    Valores.GolesEquipo2++;
            }

            ));
        }
        public void AgregarDatos(string nom1,string nom2, string t)
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
                Valores.NombreEquipo1 = nom1;
                Valores.NombreEquipo2 = nom2;
                Valores.Tiempo = t +" Tiempo";
                Timer.Start();
                if (t=="Segundo")
                {
                    time = 2700;
                }
            }

           ));
        }

       
        public void TiempoPartido(string detener)
        {
            dispatcher.BeginInvoke(new Action(() => {
              //solo cuando es mayor de 45 minutos puede detener el tiempo
                if (time >= 2700 && Valores.Tiempo=="Primer Tiempo")
                {
                    Timer.Stop();
                }
                //despues de los 90
                else if(time>=5400 && Valores.Tiempo== "Segundo Tiempo")
                {
                    Timer.Stop();
                }    
              
            }));

        }

    }
}
