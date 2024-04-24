using P2DEngine.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace P2DEngine.Games
{
    internal class Clase2304 : P2DGame
    {
        //Podemos crear diferentes tipos de figuras, todas heredan de P2DGameObject.
        P2DBlock block;
        P2DCircle circle;
        P2DTriangle triangle;

        //Queremos un bloque que siga al mouse, que hereda de P2DBlock.
        MouseBlock mouseBlock;

        //Podemos crear círculos que se vean afectados por la gravedad, que heredan de P2DCircle.
        GravityCircle gravityCircleWithDeltaTime; // Movimiento calculado con deltaTime
        GravityCircle gravityCircleWithoutDeltaTime; // Movimiento no calculado con deltaTime
        
        // Queremos identificar si presionó algunas teclas.
        bool pressedI;
        bool pressedK;

        bool pressedW;
        bool pressedA;
        bool pressedS;
        bool pressedD;

        bool pressedSpace;
        bool previousPressedSpace;

        public Clase2304(int width, int height, P2DViewport vp, int targetFPS) : base(width, height, vp, targetFPS)
        {
            // Creamos las figuras. 
            block = new P2DBlock(100, 100, 50, 50, 45, Color.White);
            circle = new P2DCircle(600, 100, 30, Color.White);

            //Ver la clase GravityCircle para conocer la diferencia entre estos dos círculos.
            gravityCircleWithDeltaTime = new GravityCircle(500, 0, 30, Color.Red, true);
            gravityCircleWithoutDeltaTime = new GravityCircle(600, 0, 30, Color.Blue, false);
            
            //Creamos el bloque que seguirá al mouse (ver la clase MouseBlock)
            mouseBlock = new MouseBlock(0,0, 30, 30, 0, Color.White);



            // Instanciarlos hará que se dibujen y se les llame a su Update cada frame, ya que se implementó una lista de GameObjects (ver la clase P2DGame).
            
            // Se puede instanciar objetos a los que ya se les llamó el constructor.
            Instantiate(mouseBlock);
            Instantiate(block);
            Instantiate(circle);
            Instantiate(gravityCircleWithDeltaTime);
            Instantiate(gravityCircleWithoutDeltaTime);

            // Pueden simplemente asignarlos mientras se construyen.
            Instantiate(triangle = new P2DTriangle(400, 400, 30, 30, 25, Color.White));
            
            // O pueden simplemente crear un nuevo elemento sin asignarlo a ninguna parte, en este caso, con una imagen.
            Instantiate(new P2DBlock(1000, 250, 100, 100, 30, P2DImageManager.Get("Background")));
            
            
        }

        // Procesar los inputs del juego.
        protected override void ProcessInput()
        {
            pressedI = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.I);
            pressedK = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.K);

            pressedW = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.W);
            pressedA = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.A);
            pressedS = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.S);
            pressedD = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.D);

            pressedSpace = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.Space);
        }

        // Dibujar los elementos del juego.
        protected override void RenderGame(Graphics g)
        {
            // Vea el método RenderGame de P2DGame.
            base.RenderGame(g);
        }

        protected override void UpdateGame()
        {
            // Vea el método UpdateGame de P2DGame.
            base.UpdateGame();


            // Nota: Varios de estos métodos están implementados en P2DGame.cs

            if(pressedI) // Cuando presionemos I, queremos que la pantalla aumente su tamaño, manteniendo su relación de aspecto.
            {
                ChangeScreenWidth(mainWindow.ClientSize.Width + 10); // También se encuentra implementado ChangeScreenHeight, para cambiar respecto a la altura.
            }
            if(pressedK) // Cuando presionemos K, queremos que la pantalla disminuya su tamaño, manteniendo su relación de aspecto.
            {
                ChangeScreenWidth(mainWindow.ClientSize.Width - 10);
            }

            if(pressedSpace && !previousPressedSpace) // Cuando presionemos espacio, queremos que la pantalla cambie directamente a esta resolución, no necesariamente siguiendo la relación de aspecto.
            {
                ChangeScreenSize(1600, 1200);
            }

            // Presionando WASD, queremos que se mueva el viewport. en las 4 direcciones correspondientes.
            if(pressedW)
            {
                viewport.Y -= 10 * DeltaTime;
            }
            else if(pressedS)
            {
                viewport.Y += 10 * DeltaTime;
            }
            else if(pressedD)
            {
                viewport.X += 10 * DeltaTime;
            }
            else if(pressedA)
            {
                viewport.X -= 10 * DeltaTime;
            }
            previousPressedSpace = pressedSpace;
        }

        
    }
}
