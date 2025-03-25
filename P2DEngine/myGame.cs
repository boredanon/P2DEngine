using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;

namespace P2DEngine
{
    // Clase que tiene la lógica del juego.
    public abstract class myGame
    {
        // La ventana.
        myWindow window;
        protected int windowWidth;
        protected int windowHeight;
        protected myCamera mainCamera;

        //Tiempo que nosotros queremos mantener. Ej. Si queremos jugar en 60 fps, deberíamos actualizar cada 16 milisegundos.
        int targetTime;

        

        // Inicializamos las variables en el constructor.
        public myGame(int width, int height, int FPS, myCamera c)
        {
            targetTime = 1000 / FPS;
            window = new myWindow(width, height); // Creamos la ventana.
            windowWidth = window.ClientSize.Width;
            windowHeight = window.ClientSize.Height;

            mainCamera = c;
        }

        public void Start()
        {
            // Mostramos la ventana.
            window.Show();

            // Recuerde, ejecutamos en un hilo distinto al principal, para no estorbar el dibujado.
            Thread t = new Thread(GameLoop);
            t.Start();
        }

        // Ciclo de juego.
        private void GameLoop()
        {
            var loop = true;
            while (loop)
            {
                Stopwatch sw = new Stopwatch();

                // Este es el ciclo de juego: Procesamos los inputs -> actualizamos valores -> pintamos.
                sw.Start();
                ProcessInput();
                Update();
                Render();
                sw.Stop();

                int deltaTime = (int)(sw.ElapsedMilliseconds); // Tiempo que ocurre durante el frame.

                // Recuerde que queremos actualizar cada "targetTime", en nuestro motor, debemos calibrarlo.
                int sleepTime = targetTime - deltaTime;
                if (sleepTime < 0)
                {
                    sleepTime = 1;
                }
                Thread.Sleep(sleepTime);

                // Si cerramos la ventana.
                if (window.IsDisposed)
                {
                    loop = false;
                }
            }
            Environment.Exit(0); // Propio de WinForms, es para cerrar la ventana.
        }

        // Primera parte del GameLoop: Procesar inputs.
        protected abstract void ProcessInput();

        // Segunda parte del GameLoop: Actualizar valores.
        protected abstract void Update();

        private void Render()
        {
            RenderGame(window.GetGraphics());
            window.Render();
        }

        // Tercera parte del GameLoop: Dibujar.
        protected abstract void RenderGame(Graphics g);
    }
}
