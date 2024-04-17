using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P2DEngine
{
    //En esta clase irá toda la lógica necesaria para hacer cualquier juego.
    public abstract class P2DGame
    {
        protected P2DWindow mainWindow; // Pantalla.

        protected int windowHeight { get; set; }
        protected int windowWidth { get; set; }
        protected int targetTime { get; set; }
        protected int FPS { get; set; }
        protected float DeltaTime { get; set; }

        // Para construir el juego, necesitamos el ancho y alto de la pantalla junto a los FPS.
        public P2DGame(int width, int height, int targetFPS)
        {
            mainWindow = new P2DWindow(width, height);
            windowHeight = height;
            windowWidth = width;
            FPS = targetFPS;
            targetTime = 1000 / targetFPS;
        }

        // Iniciamos la lógica del motor.
        public void Start()
        {
            mainWindow.Show(); // Mostramos nuestra ventana.

            Thread t = new Thread(GameLoop); // Generamos un hilo con el gameloop en otro hilo.
            t.Start(); // Iniciamos el gameloop.
        }


        public void GameLoop()
        {
            bool loop = true;

            while (loop) // Mientras siga corriendo el programa.
            {
                Stopwatch sw = new Stopwatch(); // Utilizado para medir el tiempo.

                sw.Start();
                // Estos son los elementos principales del game loop.
                ProcessInput(); // Procesamos los inputs
                UpdateGame(); // Actualizamos el estado del juego.
                RenderGame(mainWindow.GetGraphics()); // Mostramos en pantalla.
                mainWindow.Render();
                sw.Stop();

                DeltaTime = (float)sw.Elapsed.TotalMilliseconds; // Tiempo que demora en una iteración del loop.
                double sleepTime = targetTime - DeltaTime; // Cuanto debe "dormir" 

                if (sleepTime <= 0) // Dejaremos que el loop duerma mínimo un milisegundo.
                {
                    sleepTime = 1;
                }
                Thread.Sleep((int)sleepTime);

                if (mainWindow.IsDisposed) // Si cerramos la ventana, se cierra el juego.
                {
                    loop = false;
                }
            }

            Environment.Exit(0); // Propio de Forms.
        }

        // Todos los "hijos" de la clase P2DGame deberán implementar estos tres métodos, vea el archivo Arkanoid.cs para entender mejor.
        protected abstract void ProcessInput();
        protected abstract void UpdateGame();
        protected abstract void RenderGame(Graphics g);

    }
}
