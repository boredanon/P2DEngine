using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2DEngine
{
    public class NewGame : myGame
    {
        float playerX;
        float playerY;

        float playerSizeX;
        float playerSizeY;

        float enemyX;
        float enemyY;

        float enemySizeX;
        float enemySizeY;  
        public NewGame(int width, int height, int FPS, myCamera c) : base(width, height, FPS, c)
        {
            playerX = 100;
            playerY = 100;

            enemyX = 1000;
            enemyY = 200;

            playerSizeX = playerSizeY = enemySizeX = enemySizeY = 100;
        }

        protected override void ProcessInput()
        {
            int step = 10;

            // Mover al jugador.
            if(myInputManager.IsKeyPressed(Keys.W))
            {
                playerY -= step;
            }
            if (myInputManager.IsKeyPressed(Keys.A))
            {
                playerX -= step;
            }
            if (myInputManager.IsKeyPressed(Keys.S))
            {
                playerY += step;
            }
            if (myInputManager.IsKeyPressed(Keys.D))
            {
                playerX += step;
            }

            // Mover a la cámara.
            if (myInputManager.IsKeyPressed(Keys.Up))
            {
                mainCamera.y -= step;
            }
            if (myInputManager.IsKeyPressed(Keys.Down))
            {
                mainCamera.y += step;
            }
            if (myInputManager.IsKeyPressed(Keys.Left))
            {
                mainCamera.x -= step;
            }
            if (myInputManager.IsKeyPressed(Keys.Right))
            {
                mainCamera.x += step;
            }

            // Hacer zoom.
            if(myInputManager.IsKeyPressed(Keys.Z))
            {
                mainCamera.zoom += 0.01f;
            }
            if(myInputManager.IsKeyPressed (Keys.X))
            {
                mainCamera.zoom -= 0.01f;
            }
        }

        protected override void RenderGame(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, windowWidth, windowHeight);


            // Añadimos las variables de mainCamera a la lógica de dibujado. Estamos
            // dibujando con respecto a la cámara.
            g.FillRectangle(new SolidBrush(Color.Black), 
                (playerX - mainCamera.x) * mainCamera.zoom, 
                (playerY - mainCamera.y) * mainCamera.zoom, 
                playerSizeX * mainCamera.zoom, 
                playerSizeY * mainCamera.zoom);

            g.FillRectangle(new SolidBrush(Color.Blue), 
                (enemyX - mainCamera.x) * mainCamera.zoom, 
                (enemyY - mainCamera.y) * mainCamera.zoom, 
                enemySizeX * mainCamera.zoom, 
                enemySizeY * mainCamera.zoom);
            //throw new NotImplementedException();
        }

        protected override void Update()
        {
            // Centrar la cámara con respecto al player.
            mainCamera.x = playerX - (windowWidth / (2 * mainCamera.zoom));
            mainCamera.y = playerY - (windowHeight / (2 * mainCamera.zoom));
            //throw new NotImplementedException();
        }
    }
}
