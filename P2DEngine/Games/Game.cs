using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Games
{
    public class Game : myGame
    {
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
        }

        protected override void ProcessInput()
        {
        }

        protected override void RenderGame(Graphics g)
        {
        }

        protected override void Update()
        {
        }
    }
}
