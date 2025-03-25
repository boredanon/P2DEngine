using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;

namespace P2DEngine
{
    // Toda la lógica del pong la guardamos en su propia clase.
    public class PongGame : myGame
    {
        // Posición del jugador.
        int playerX = 0;
        int playerY = 0;


        int enemyX;
        int enemyY;
        int enemyDy;

        // Posición de la pelota.
        int ballX;
        int ballY;

        // Dirección de la pelota (ya que esta rebota).
        int ballDx;
        int ballDy;

        // Tamaño de la pelota.
        int ballSizeX;
        int ballSizeY;

        int pointsPlayer;
        int pointsEnemy;

        public PongGame(int width, int height, int FPS, myCamera c) : base(width, height, FPS, c)
        {
            playerX = 20;

            enemyX = width - 30;
            enemyY = height / 2;
            enemyDy = 1;

            // Posicionamos la pelota.
            ballX = width / 2;
            ballY = height / 2;

            ballDx = 1;
            ballDy = 1;

            ballSizeX = 20;
            ballSizeY = 20;

            pointsPlayer = 0;
            pointsEnemy = 0;
        }

        protected override void ProcessInput()
        {

                // Ud. también podría tener una variable y actualizar los valores en el Update.

                int step = 10;
                if (myInputManager.IsKeyPressed(Keys.W) && playerY > 0)
                {
                    playerY -= step;
                }
                if (myInputManager.IsKeyPressed(Keys.S) && playerY + 100 < windowHeight)
                {
                    playerY += step;
                }
            
        }
        protected override void Update()
        {
                int step = 10;

                // Movemos la pelota automáticamente.
                ballX += ballDx * step;
                ballY += ballDy * step;

                enemyY += enemyDy * step;

                if (enemyY + 100 > windowHeight)
                {
                    enemyDy = -1;
                }
                if (enemyY < 0)
                {
                    enemyDy = 1;
                }


                // Lógica de rebote de la pelota. Recuerde que la esquina superior izquierda es el 0,0.
                if (ballY + ballSizeY >= windowHeight)
                {
                    ballDy = -1;
                }
                if (ballY <= 0)
                {
                    ballDy = 1;
                }

                //Console.WriteLine(ballSizeX + ballX);

                if (ballX + ballSizeX >= windowWidth)
                {
                    ballX = windowWidth / 2;
                    ballY = windowHeight / 2;

                    pointsPlayer += 1;
                }

                if (ballX <= 0)
                {
                    ballX = windowWidth / 2;
                    ballY = windowHeight / 2;

                    pointsEnemy += 1;
                }

                if (ballX <= playerX + 20
                    && ballY > playerY
                    && ballY < playerY + 100)
                {
                    ballDx = 1;
                }

                if (ballX + ballSizeX >= enemyX
                    && ballY > enemyY
                    && ballY < enemyY + 100)
                {
                    ballDx = -1;
                }

            
        }
        protected override void RenderGame(Graphics g)
        {
            
                //Dibujamos un fondo.
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, windowWidth, windowHeight);

                for (int i = 0; i < 100; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.Black), windowWidth / 2, 0 + i * 20, 2, 10);
                }

                //Dibujamos a nuestro player.
                g.FillRectangle(new SolidBrush(Color.Black), playerX, playerY, 20, 100);
                g.FillRectangle(new SolidBrush(Color.Black), enemyX, enemyY, 20, 100);

                //Dibujamos la pelota.
                g.FillEllipse(new SolidBrush(Color.Red), ballX, ballY, ballSizeX, ballSizeY);


                Font font = new Font("Calibri", 20, FontStyle.Bold);
                g.DrawString(pointsPlayer.ToString(),
                    font, new SolidBrush(Color.Black), 50, 50);

                g.DrawString(pointsEnemy.ToString(),
                    font, new SolidBrush(Color.Black), windowWidth - 100, 50);
            
        }
    }
}
