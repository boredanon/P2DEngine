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
    public class P2DGame
    {
        // Ventana que se va a mostrar.
        P2DWindow mainWindow;

        // Posicion del rectángulo en la pantalla.
        private int rectangleX { get; set; }
        private int rectangleY { get; set; }


        // Posición del círculo en la pantalla.
        public int ball_x { get; set; }
        public int ball_y { get; set; }

        // Velocidad del círculo en el eje x e y.
        public int ball_dx { get; set; }
        public int ball_dy {  get; set; }

        // Es recomentable utilizar el constructor para inicializar las variables.
        public P2DGame(int width, int height) {
            mainWindow = new P2DWindow(width, height);

            // Centramos la pelota.
            ball_x = width / 2;
            ball_y = height / 2;

            // La velocidad inicial.
            ball_dx = 1;
            ball_dy = 1;    
        }

        public void Start()
        {
            mainWindow.Show(); // Mostramos nuestra ventana.

            Thread t = new Thread(GameLoop); // Generamos un hilo con el gameloop en otro hilo.
            t.Start(); // Iniciamos el gameloop.
        }

        public void GameLoop()
        {
            bool loop = true;

            while(loop) // Mientras siga corriendo el programa.
            {
                Stopwatch sw = new Stopwatch(); // Utilizado para medir el tiempo.

                sw.Start();
                // Estos son los elementos principales del game loop.
                ProcessInput(); // Procesamos los inputs
                UpdateGame(); // Actualizamos el estado del juego.
                RenderGame(); // Mostramos en pantalla.
                sw.Stop(); 

                int deltaTime = (int)sw.ElapsedMilliseconds; // Tiempo que demora en una iteración del loop.

                int sleepTime = (1000 / 60) - deltaTime; // Cuanto debe "dormir" 

                if(sleepTime < 0) // Dejaremos que el loop duerma mínimo un milisegundo.
                {
                    sleepTime = 1; 
                }
                Thread.Sleep(sleepTime);

                if (mainWindow.IsDisposed) // Si cerramos la ventana, se cierra el juego.
                {
                    loop = false;
                }
            }

            Environment.Exit(0); // Propio de Forms.
        }

        private void RenderGame() // Lógica de dibujado.
        {
            Graphics g = mainWindow.CreateGraphics();

            // Pintamos el fondo.
            g.FillRectangle(new SolidBrush(Color.Purple), 0, 0, mainWindow.ClientSize.Width, mainWindow.ClientSize.Height);

            // Rectángulo.
            g.FillRectangle(new SolidBrush(Color.Black), rectangleX, rectangleY, 20, 100);

            // Círculo
            g.FillEllipse(new SolidBrush(Color.Black), ball_x, ball_y, 20, 20);

        }

        private void UpdateGame() // Lógica de juego.
        {
            // Cuantas unidades queremos que se mueva.
            int step = 10;

            // Movemos el balón.
            ball_x += ball_dx * step;
            ball_y += ball_dy * step;

            // Queremos que rebote en los bordes.
            if(ball_x < 0)
            {
                ball_dx = 1;
            }else if(ball_x > mainWindow.ClientSize.Width)
            {
                ball_dx = -1;
            }

            if(ball_y < 0)
            {
                ball_dy = 1;
            }
            else if(ball_y > mainWindow.ClientSize.Width)
            {
                ball_dy = -1;
            }
        }

        private void ProcessInput() // Procesamos la entrada que realiza el usuario.
        {
            int step = 10;

            if (mainWindow.IsKeyPressed(Keys.Right))
            {
                rectangleX += step;
            }
            if (mainWindow.IsKeyPressed(Keys.Left))
            {
                rectangleX -= step;
            }
            if (mainWindow.IsKeyPressed(Keys.Up))
            {
                rectangleY -= step;
            }
            if (mainWindow.IsKeyPressed(Keys.Down))
            {
                rectangleY += step;
            }
        }
    }
}
