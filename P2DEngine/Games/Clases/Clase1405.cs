using P2DEngine.Engine;
using P2DEngine.Engine.GameComponents;
using P2DEngine.Engine.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Games.Clases
{
    public class Clase1405 : P2DGame
    {
        bool pressedA; // Si presionamos la tecla A...
        bool pressedD; // Si presionamos la tecla D...

        Player player; // Un player cualquiera...

        P2DSprite sprite; // Animación #1.
        P2DSprite otherSprite; // Animación #2.

        public Clase1405(int width, int height, P2DViewport vp, int targetFPS) : base(width, height, vp, targetFPS)
        {
            //Añadimos las distintas capas para el fondo (vea los Assets en Assets/Images/Parallax)
            //Notemos que ahora queremos actuar sobre distintos elementos del fondo de distinta manera, por lo que
            //tenemos que separarlo en capas.
            P2DBackgroundManager.AddLayer("bg_1", 0.1f); // Recuerde asignar la escala.
            P2DBackgroundManager.AddLayer("bg_2", 0.25f); // Mientras se acerca, más rápido se debería mover.
            P2DBackgroundManager.AddLayer("bg_3", 0.50f);
            P2DBackgroundManager.AddLayer("bg_4", 1.0f);

            // Inicializamos los sprites con las respectivas imágenes cargadas en Program.cs
            sprite = new P2DSprite(0.5f); // Cada 0.5f que cambie.
            for (int i = 0; i < 3; i++)
            {
                sprite.Add(P2DImageManager.Get("player_" + i));
            }

            otherSprite = new P2DSprite(0.2f); // Cada 0.2f que cambie.
            for(int i = 0; i < 2; i++)
            {
                otherSprite.Add(P2DImageManager.Get("other_" + i));
            }

            // Instanciamos el player.
            Instantiate(player = new Player(200, 200, 200, 200, 0, sprite));
        }

        protected override void ProcessInput()
        {
            pressedA = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.A);
            pressedD = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.D);
        }

        protected override void UpdateGame()
        {
            base.UpdateGame();

            if (pressedA) // Movemos el fondo.
            {
                P2DBackgroundManager.SetParallaxSpeed(-1000f);
                player.currentSprite = otherSprite; // Cambiamos la animación.
            }
            else if(pressedD) 
            {
                P2DBackgroundManager.SetParallaxSpeed(1000f);
                player.currentSprite = otherSprite; // Cambiamos la animación.
            }
            else
            {
                P2DBackgroundManager.SetParallaxSpeed(0);
                player.currentSprite = sprite; // Animación original.
            }
            
            //Desafío: Asigne currentSprite mediante un "switch".
            /* Ejemplo:
             * 
             * switch(player_state):
             *      case idle:
             *          currentSprite = sprite1;
             *      break;
             *      case walk:
             *          currentSprite = sprite2;
             *      break;
             *      (...)
             */
        }

        protected override void RenderGame(Graphics g)
        {
            base.RenderGame(g);
        }
    }
}
