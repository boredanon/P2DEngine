using P2DEngine.Managers;
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
    // Clase que tiene la lógica del juego. Eliminamos todo lo del manejo de gameObjects, ya que se movió a la clase myScene.
    public abstract class myGame
    {
        // La ventana.
        myWindow window;
        public static int windowWidth;
        public static int windowHeight;
        

        //Tiempo que nosotros queremos mantener. Ej. Si queremos jugar en 60 fps, deberíamos actualizar cada 16 milisegundos.
        int targetTime;
        protected float deltaTime;



        // Inicializamos las variables en el constructor.
        public myGame(int width, int height, int FPS, myCamera c)
        {
            targetTime = 1000 / FPS;
            window = new myWindow(width, height); // Creamos la ventana.
            windowWidth = window.ClientSize.Width;
            windowHeight = window.ClientSize.Height;
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
                UpdateGame();
                Render(window.GetGraphics());
                sw.Stop();

                int frameTime = (int)(sw.ElapsedMilliseconds); // Tiempo que ocurre durante el frame.

                // Recuerde que queremos actualizar cada "targetTime", en nuestro motor, debemos calibrarlo.
                int sleepTime = targetTime - frameTime;

                deltaTime = (sleepTime + frameTime)/1000.0f; // Esto es aproximadamente el tiempo en segundos entre
                // cada frame.

                
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
        protected virtual void ProcessInput()
        {
            mySceneManager.GetActiveScene().ProcessInput();
        }

        // Segunda parte del GameLoop: Actualizar valores.
        protected virtual void Update()
        {
            mySceneManager.GetActiveScene().Update(deltaTime);
        }

        protected virtual void Render(Graphics g) // Dibujamos la escena actual.
        {
            var activeScene = mySceneManager.GetActiveScene();
            var backgroundLayers = myBackgroundManager.GetLayers(activeScene);
            foreach(var layer in backgroundLayers)
            {
                layer.Draw(g, activeScene.currentCamera);
            }
            foreach(var gameObject in activeScene.gameObjects)
            {
                gameObject.Draw(g,
                    activeScene.currentCamera.GetViewPosition(gameObject.x, gameObject.y),
                    activeScene.currentCamera.GetViewSize(gameObject.sizeX, gameObject.sizeY));
            }
            mySceneManager.GetActiveScene().Render(g);
            window.Render();
        }


        protected void UpdateGame() // Actualizamos los valores solo de la escena actual.
        {
            var activeScene = mySceneManager.GetActiveScene();
            var backgroundLayers = myBackgroundManager.GetLayers(activeScene);
            foreach(var layer in backgroundLayers)
            {
                layer.Update(deltaTime);
            }
            foreach (var gameObjects in activeScene.gameObjects)
            {
                gameObjects.Update(deltaTime);
            }
            Update();
        }
    }
}
