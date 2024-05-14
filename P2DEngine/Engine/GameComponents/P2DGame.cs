using P2DEngine.Engine;
using P2DEngine.Engine.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace P2DEngine
{
    //En esta clase irá toda la lógica necesaria para hacer cualquier juego.
    public abstract class P2DGame
    {
        protected P2DWindow mainWindow; // Pantalla.

        protected int windowHeight { get; set; }
        protected int windowWidth { get; set; }
        protected float targetTime { get; set; }
        protected int FPS { get; set; }
        protected float DeltaTime { get; set; }

        protected P2DViewport viewport { get; set; }

        protected PointF oldViewportSize { get; set; }

        public static PointF viewportSize { get; set;}

        protected List<P2DGameObject> gameObjects { get; set; } // Lista de gameobjects para mostrar en pantalla y actualizar.

        // Para construir el juego, necesitamos el ancho y alto de la pantalla junto a los FPS.
        public P2DGame(int width, int height, P2DViewport vp, int targetFPS)
        {
            mainWindow = new P2DWindow(width, height);
            gameObjects = new List<P2DGameObject>();    
            windowHeight = height;
            windowWidth = width;

            FPS = targetFPS;
            viewport = vp;
            viewportSize = new PointF(viewport.Width, viewport.Height);
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

                DeltaTime *= 0.001f;
                if (mainWindow.IsDisposed) // Si cerramos la ventana, se cierra el juego.
                {
                    loop = false;
                }
            }

            Environment.Exit(0); // Propio de Forms.
        }

        protected void Instantiate(P2DGameObject gameObject) // Añadir un objeto a la lista.
        {
            gameObjects.Add(gameObject);
        }

        protected void Destroy(P2DGameObject gameObject) // Remover un objeto de la lista.
        {
            gameObjects.Remove(gameObject);
        }


        // Todos los "hijos" de la clase P2DGame deberán implementar estos tres métodos, vea el archivo Arkanoid.cs para entender mejor.
        protected abstract void ProcessInput();
        protected virtual void UpdateGame()
        {
            foreach (var layer in P2DBackgroundManager.layers) // Actualizamos cada layer del fondo.
            {
                layer.Update(DeltaTime);
            }

            foreach (var gameObject in gameObjects) // Actualizamos cada objeto de la lista.
            {
                gameObject.Update(DeltaTime);
            }

            oldViewportSize = new PointF(viewport.Width, viewport.Height); // Actualizamos los tamaños del viewport.
        }
        protected virtual void RenderGame(Graphics g)
        {
            // Calculamos la relación de aspecto del viewport.
            var xRatio = viewport.Width / oldViewportSize.X;
            var yRatio = viewport.Height / oldViewportSize.Y;

            foreach(var layer in P2DBackgroundManager.layers) // Dibujamos el fondo primero.
            {
                var position = layer.Position;
                layer.Position = new PointF(layer.Position.X - viewport.X * xRatio,
                                                layer.Position.Y - viewport.Y * yRatio);
                layer.Draw(g);
                layer.Position = position;
            }


            foreach (var gameObject in gameObjects) // Tenemos que dibujar cada elemento con respecto a su posición en el viewport ahora, piense en el viewport como una cámara.
            {
                var position = gameObject.Position;
                gameObject.Position = new PointF(gameObject.Position.X - viewport.X * xRatio,
                                                gameObject.Position.Y - viewport.Y * yRatio);
                gameObject.Draw(g);
                gameObject.Position = position;
            }
        }

        // Cambiar los tamaños de la ventana, respetando la relación de aspecto.
        protected void ChangeScreenWidth(int width)
        {
            mainWindow.ChangeWidth(mainWindow, width);
            viewport.Width = mainWindow.ClientSize.Width;
            viewport.Height = mainWindow.ClientSize.Height;

            viewportSize = new PointF(viewport.Width, viewport.Height);

            RescaleElements();
        }

        protected void ChangeScreenHeight(int height)
        {
            mainWindow.ChangeHeight(mainWindow, height);
            viewport.Width = mainWindow.ClientSize.Width;
            viewport.Height = mainWindow.ClientSize.Height;

            viewportSize = new PointF(viewport.Width, viewport.Height);

            RescaleElements();
        }

        // Cambiar directamente el tamaño de la ventana, sin necesariamente respetar la relación de aspecto.
        protected void ChangeScreenSize(int width, int height)
        {
            mainWindow.ChangeSize(mainWindow, width, height);
            viewport.Width = mainWindow.ClientSize.Width;
            viewport.Height = mainWindow.ClientSize.Height;

            viewportSize = new PointF(viewport.Width, viewport.Height);

            RescaleElements();
        }

        
        // Cada vez que cambiamos el tamaño de la ventana, debemos preocuparnos de que los objetos puedan escalarse al tamaño de la nueva ventana.
        protected void RescaleElements()
        {
            // Relación de aspecto del viewport.
            var xRatio = viewport.Width / oldViewportSize.X;
            var yRatio = viewport.Height / oldViewportSize.Y;

            foreach(var layer in P2DBackgroundManager.layers)
            {
                var newWidth = layer.Size.X * xRatio;
                var newHeight = layer.Size.Y * yRatio;

                layer.Size = new PointF(newWidth, newHeight);
                layer.Position = new PointF((float)(layer.Position.X * xRatio), (float)((layer.Position.Y * yRatio)));
            }


            foreach (P2DGameObject gameObject in gameObjects) // Reescalamos cada GameObject.
            {
                var newWidth = gameObject.Size.X * xRatio;
                var newHeight = gameObject.Size.Y * yRatio;

                gameObject.Size = new PointF(newWidth, newHeight);
                gameObject.Position = new PointF((float)(gameObject.Position.X * xRatio), (float)((gameObject.Position.Y * yRatio)));
            }
        }

    }
}
