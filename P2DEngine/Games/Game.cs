using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2DEngine.GameObjects;
using P2DEngine.GameObjects.Collisions;

namespace P2DEngine.Games
{
    public class Game : myGame
    {
        public Game(int width, int height, int FPS, myCamera c) : base(width, height, FPS, c)
        {
            /*
             * Para hacer animaciones, ud. tiene que;
             * 1) Crear un objeto de tipo sprite.
             * 2) Añadirle imágenes.
             * 3) Asignarlas a un gameobject/physicsgameobject.
             */
        }

        protected override void ProcessInput()
        {
        }

        // Hice un pequeño cambio en el código de myGame.cs, ahora no deberían tener que escribir el loop
        // para dibujar cada objeto en cada juego que hacen. Sin embargo, si quieren hacer juegos de cámaras deben buscar la forma
        // de hacerlo uds. cambiando la variable "currentCamera".
        protected override void RenderGame(Graphics g)
        {
        }

        // Lo mismo para el Update.
        protected override void Update()
        {
            // En este motor, solo los physicsGameObject tienen colisión.
        }
    }
}
