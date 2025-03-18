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
    public class myGame
    {
        // La ventana.
        myWindow window;

        // Posición del jugador.
        int playerX = 0;
        int playerY = 0;

        // Posición de la pelota.
        int ballX;
        int ballY;

        // Dirección de la pelota (ya que esta rebota).
        int ballDx;
        int ballDy;

        // Tamaño de la pelota.
        int ballSizeX;
        int ballSizeY;

        //Tiempo que nosotros queremos mantener. Ej. Si queremos jugar en 60 fps, deberíamos actualizar cada 16 milisegundos.
        int targetTime;
        
        // Inicializamos las variables en el constructor.
        public myGame(int width, int height, int FPS) {
            
            targetTime = 1000 / FPS;
            window = new myWindow(width, height); // Creamos la ventana.

            // Posicionamos la pelota.
            ballX = width / 2;
            ballY = height / 2;

            ballDx = 1;
            ballDy = 1;

            ballSizeX = 20;
            ballSizeY = 20;
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
        public void GameLoop()
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
                if(window.IsDisposed)
                {
                    loop = false;
                }
            }
            Environment.Exit(0); // Propio de WinForms, es para cerrar la ventana.
        }

        // Primera parte del GameLoop: Procesar inputs.
        private void ProcessInput()
        {
            // Ud. también podría tener una variable y actualizar los valores en el Update.
            
            int step = 10;
            if (window.IsKeyPressed(Keys.W)) 
            {
                playerY -= step;
            }


            if(window.IsKeyPressed(Keys.S))
            {
                playerY += step;
            }
        }

        // Segunda parte del GameLoop: Actualizar valores.
        public void Update()
        {
            int step = 10;

            // Movemos la pelota automáticamente.
            ballX += ballDx * step;
            ballY += ballDy * step;
            
            // Lógica de rebote de la pelota. Recuerde que la esquina superior izquierda es el 0,0.
            if(ballY >= window.Height)
            {
                ballDy = -1;
            }
            if(ballY <= 0)
            {
                ballDy = 1;
            }

            if(ballX >= window.Width)
            {
                ballDx = -1;
            }

            if (ballX <= 0)
            {
                ballDx = 1;
            }
        }

        // Tercera parte del GameLoop: Dibujar.
        public void Render()
        {
            Graphics g = window.CreateGraphics(); // Propio de WinForms para dibujar.

            //Dibujamos un fondo.
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, window.Width, window.Height);
            
            //Dibujamos a nuestro player.
            g.FillRectangle(new SolidBrush(Color.Black), playerX, playerY, 20, 100);

            //Dibujamos la pelota.
            g.FillEllipse(new SolidBrush(Color.Red), ballX, ballY, ballSizeX, ballSizeY);
        }
    }
}
