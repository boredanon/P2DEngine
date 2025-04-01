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
        Player player;
        Enemy enemy;

        Random rand = new Random();

        bool previousPressedSpace = false;

        myCamera camera2;

        public NewGame(int width, int height, int FPS, myCamera c) : base(width, height, FPS, c)
        {
            player = new Player(100, 100, 100, 100, Color.Red);
            enemy = new Enemy(100, 220, 100, 100, Color.Blue);
            
            Instantiate(enemy);
            Instantiate(player);

            camera2 = new myCamera(0, 0, 800, 600, 1.0f);
        }



        protected override void ProcessInput()
        {
            int step = 100;

            // Mover al jugador.
            if (myInputManager.IsKeyPressed(Keys.W))
            {
                player.y -= step;
            }
            if (myInputManager.IsKeyPressed(Keys.A))
            {
                player.x -= step;
            }
            if (myInputManager.IsKeyPressed(Keys.S))
            {
                player.y += step;
            }
            if (myInputManager.IsKeyPressed(Keys.D))
            {
                player.x += step;
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

            var pressedSpace = myInputManager.IsKeyPressed(Keys.Space);

            if (myInputManager.IsKeyPressed(Keys.Space) && !previousPressedSpace)
            {
                // var newObject = new myBlock(rand.Next(100), rand.Next(100), 100, 100, Color.Purple);
                //               Instantiate(newObject);
                currentCamera = camera2;
            }

            // Hacer zoom.
            if (myInputManager.IsKeyPressed(Keys.Z))
            {
                mainCamera.zoom += 0.01f;
            }
            if (myInputManager.IsKeyPressed(Keys.X))
            {
                mainCamera.zoom -= 0.01f;
            }

            previousPressedSpace = pressedSpace;
        }

        protected override void RenderGame(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, windowWidth, windowHeight);
            // Añadimos las variables de mainCamera a la lógica de dibujado. Estamos
            // dibujando con respecto a la cámara.

            foreach(var gameObject in gameObjects)
            {
                gameObject.Draw(g, 
                    currentCamera.GetViewPosition(gameObject.x, gameObject.y),
                    currentCamera.GetViewSize(gameObject.sizeX, gameObject.sizeY));
            }
        }

        protected override void Update()
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Update(deltaTime);
            }

            currentCamera.x = player.x - (windowWidth / (2 * currentCamera.zoom));
            currentCamera.y = player.y - (windowHeight / (2 * currentCamera.zoom));

        }
    }
}
