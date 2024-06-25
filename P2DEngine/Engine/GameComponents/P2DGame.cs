using P2DEngine.Engine;
using P2DEngine.Engine.GameComponents;
using P2DEngine.Engine.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
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
        protected static P2DWindow mainWindow; // Pantalla.

        protected int windowHeight { get; set; }
        protected int windowWidth { get; set; }
        protected float targetTime { get; set; }
        protected int FPS { get; set; }
        protected float DeltaTime { get; set; }

        public static P2DViewport viewport { get; set; }

        protected static PointF oldViewportSize { get; set; }

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

        

        // Todos los "hijos" de la clase P2DGame deberán implementar estos tres métodos, vea el archivo Arkanoid.cs para entender mejor.
        protected virtual void ProcessInput()
        {
            // Solo procesamos el input de la escena actual. 
            P2DSceneManager.GetActive().ProcessInput();

            // DESAFÍO: Note que el método es "virtual", ¿cómo haría para que en ciertos juegos el input
            // se pueda manejar en múltiples escenas?
        }

        protected virtual void UpdateGame()
        {
            // Actualizamos los tamaños del viewport. Si lo quita se rompe el juego.
            oldViewportSize = new PointF(viewport.Width, viewport.Height); 
            P2DSceneManager.GetActive().Update(DeltaTime);
        }

        protected void RenderGame(Graphics g)
        {
            //Pintamos la escena actual.
            P2DSceneManager.GetActive().Render(g);
        }

        // Cambiar los tamaños de la ventana, respetando la relación de aspecto.
        public static void ChangeScreenWidth(int width)
        {
            mainWindow.ChangeWidth(mainWindow, width);
            viewport.Width = mainWindow.ClientSize.Width;
            viewport.Height = mainWindow.ClientSize.Height;

            viewportSize = new PointF(viewport.Width, viewport.Height);

            RescaleElements();
        }

        public static void ChangeScreenHeight(int height)
        {
            mainWindow.ChangeHeight(mainWindow, height);
            viewport.Width = mainWindow.ClientSize.Width;
            viewport.Height = mainWindow.ClientSize.Height;

            viewportSize = new PointF(viewport.Width, viewport.Height);

            RescaleElements();
        }

        // Cambiar directamente el tamaño de la ventana, sin necesariamente respetar la relación de aspecto.
        public static void ChangeScreenSize(int width, int height)
        {
            mainWindow.ChangeSize(mainWindow, width, height);
            viewport.Width = mainWindow.ClientSize.Width;
            viewport.Height = mainWindow.ClientSize.Height;

            viewportSize = new PointF(viewport.Width, viewport.Height);

            RescaleElements();
        }

        // Cada vez que cambiamos el tamaño de la ventana, debemos preocuparnos de que los objetos puedan escalarse al tamaño de la nueva ventana.
        public static void RescaleElements()
        {
            // Actualizamos los datos para todas las escenas y background layer.
            Dictionary<string, P2DScene> scenes = P2DSceneManager.GetScenes();
            var layers = P2DBackgroundManager.layers;
            
            // Recuerde que "var" es casteo automático: si yo digo var i = 1, i será un int, var j = 1.0f, j es float.

            foreach(KeyValuePair<string, P2DScene> kvp in scenes)
            {
                var scene = kvp.Value;
                var sceneObjects = scene.gameObjects;
                var sceneUIObjects = scene.UIObjects;
                foreach( var sceneObject in sceneObjects) // Reescalamos todos los objetos.
                {
                    RescaleObject(sceneObject);
                }

                foreach( var sceneObject in sceneUIObjects)
                {
                    RescaleObject(sceneObject);
                }
            }

            foreach (var layer in layers) // Reescalamos todas las layers
            {
                RescaleLayer(layer);
            }

        }

        // Métodos auxiliares para ayudar con los cambios de tamaño de ventana con respecto a los gameObjects.
        // uiObjects y background layers.
        public static PointF ObjectToScreen(P2DGameObject obj)
        {
            var xRatio = P2DGame.viewport.Width / oldViewportSize.X;
            var yRatio = P2DGame.viewport.Y / oldViewportSize.Y;

            var position = obj.Position;
            return new PointF(position.X - (viewport.X * xRatio), position.Y - (viewport.Y * yRatio));
        }

        public static PointF ObjectToScreen(P2DUIObject obj)
        {
            var xRatio = P2DGame.viewport.Width / oldViewportSize.X;
            var yRatio = P2DGame.viewport.Height / oldViewportSize.Y;

            var position = obj.Position;
            return new PointF(position.X - viewport.X * xRatio, position.Y - viewport.Y * yRatio);
        }

        public static PointF LayerToScreen(P2DBackgroundLayer go)
        {
            var xRatio = P2DGame.viewport.Width / P2DGame.oldViewportSize.X;
            var yRatio = P2DGame.viewport.Height / P2DGame.oldViewportSize.Y;

            var position = go.Position;
            return new PointF(position.X - viewport.X * xRatio, position.Y - viewport.Y * yRatio);
        }

        public static void RescaleObject(P2DGameObject go)
        {
            var xRatio = P2DGame.viewport.Width / P2DGame.oldViewportSize.X;
            var yRatio = P2DGame.viewport.Height / P2DGame.oldViewportSize.Y;

            var newWidth = go.Size.X * xRatio;
            var newHeight = go.Size.Y * yRatio;

            go.Size = new PointF(newWidth, newHeight);
            go.Position = new PointF((float)(go.Position.X * xRatio), (float)(go.Position.Y * yRatio));
        }

        public static void RescaleObject(P2DUIObject go)
        {
            var xRatio = P2DGame.viewport.Width / P2DGame.oldViewportSize.X;
            var yRatio = P2DGame.viewport.Height / P2DGame.oldViewportSize.Y;

            var newWidth = go.Size.X * xRatio;
            var newHeight = go.Size.Y * yRatio;

            go.Size = new PointF(newWidth, newHeight);
            go.Position = new PointF((float)(go.Position.X * xRatio), (float)(go.Position.Y * yRatio));
        }

        public static void RescaleLayer(P2DBackgroundLayer go)
        {
            var xRatio = P2DGame.viewport.Width / P2DGame.oldViewportSize.X;
            var yRatio = P2DGame.viewport.Height / P2DGame.oldViewportSize.Y;

            var newWidth = go.Size.X * xRatio;
            var newHeight = go.Size.Y * yRatio;

            go.Size = new PointF(newWidth, newHeight);
            go.Position = new PointF((float)(go.Position.X * xRatio), (float)(go.Position.Y * yRatio));
        }

    }
}
