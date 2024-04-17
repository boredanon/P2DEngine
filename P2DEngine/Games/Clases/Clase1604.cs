using P2DEngine.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace P2DEngine.Games.Arkanoid
{
    internal class Clase1604 : P2DGame
    {
        //Podemos crear diferentes tipos de figura ahora.
        P2DBlock block;
        P2DCircle circle;
        P2DTriangle triangle;

        //Bloque que siga al mouse.
        MouseBlock mouseBlock;

        //Podemos crear círculos que se vean afectados por la gravedad.
        GravityCircle gravityCircleWithDeltaTime;
        GravityCircle gravityCircleWithoutDeltaTime;


        public Clase1604(int width, int height, int targetFPS) : base(width, height, targetFPS)
        {
            // Creamos las figuras. 
            block = new P2DBlock(100, 100, 50, 50, Color.White);
            circle = new P2DCircle(600, 100, 30, Color.White);
            triangle = new P2DTriangle(400, 400, 30, 30, Color.White);

            //Creamos círculos que utilicen el deltatime o no para su movimiento (ver la clase GravityCircle)
            gravityCircleWithDeltaTime = new GravityCircle(500, 0, 30, Color.Red, true);
            gravityCircleWithoutDeltaTime = new GravityCircle(600, 0, 30, Color.Blue, false);
            
            //Creamos el bloque que seguirá al mouse (ver la clase MouseBlock)
            mouseBlock = new MouseBlock(0,0, 30, 30, Color.White);

            
        }

        // Procesar los inputs del juego.
        protected override void ProcessInput()
        {
        }

        // Dibujar los elementos del juego.
        protected override void RenderGame(Graphics g)
        {
            block.Draw(g);
            circle.Draw(g);
            mouseBlock.Draw(g);
            gravityCircleWithDeltaTime.Draw(g);
            gravityCircleWithoutDeltaTime.Draw(g);
            triangle.Draw(g);

            /* Importante:
            *  Como puede ver, está la opción de dibujar todos los elementos de juego por separado. Para ahorrarse líneas de código, una sugerencia es
            *  crear una List<P2DGameObject> y hacer que dibuje todos los elementos en un solo ciclo for (una List<P2DGameObject> aceptará objetos de tipo P2DBlock, P2DCircle, etc.)
            */
        }

        protected override void UpdateGame()
        {

            /*
             * Lo mismo que está escrito en el método RenderGame aplica para el Update. 
             */
            mouseBlock.Update(DeltaTime);
            gravityCircleWithDeltaTime.Update(DeltaTime);
            gravityCircleWithoutDeltaTime.Update(DeltaTime);


            if(mouseBlock.IsColliding(block) || mouseBlock.IsColliding(circle))
            {
                mouseBlock.SetColor(Color.Red);
            }
            else
            {
                mouseBlock.SetColor(Color.White);
            }


            
        }
    }
}
