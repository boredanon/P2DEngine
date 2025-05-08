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
        //Tenemos dos clases distintas, myPhysicsFigure y myFigure.
        myPhysicsCircle physicsCircle; 
        myCircle circle;
        
        myPhysicsBlock physicsBlock;
        myBlock block;
        public Game(int width, int height, int FPS, myCamera c) : base(width, height, FPS, c)
        {
            //Image i = myImageManager.Get("imageId"); <-- Obtener una imagen. Ojo que retorna la referencia.
            //Font f = myFontManager.Get("fontId") <-- Obtener una fuente.


            //int audioIndex = myAudioManager.Play("audio", volumen) <- Ejecutar un sonido.
            /* 
             * int audioIndex;
             * Task.Run(async () => {
             *      audioIndex = await myAudioManager.PlayAsync("audio", volumen);
             * }); <- Ejecutar un sonido asíncrono.
             */


            // Las podemos instanciar de la misma forma que hemos hecho hasta ahora.
            physicsCircle = new myPhysicsCircle(140, 140, 20, Color.Red);
            circle = new myCircle(200, 140, 20, Color.Red);
            physicsBlock = new myPhysicsBlock(140, 300, 40, 40, Color.Blue);
            block = new myBlock(200, 300, 40, 40, Color.Blue);
            
            
            physicsCircle.affectedByGravity = true;
            
            Instantiate(physicsCircle);
            Instantiate(physicsBlock);

            Instantiate(circle);
            Instantiate(block);
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
            if(physicsCircle.IsColliding(physicsBlock))
            {
                physicsCircle.SetColor(Color.Blue);
                physicsBlock.SetColor(Color.Red);
            }
            else
            {
                physicsCircle.SetColor(Color.Red);
                physicsBlock.SetColor(Color.Blue);
            }

            circle.y += 9.8f * deltaTime;
        }
    }
}
