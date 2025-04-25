using P2DEngine.Games;
using P2DEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2DEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Ancho y alto de la ventana.
            int windowWidth = 800;
            int windowHeight = 600;

            // Ancho y alto de la cámara.
            int camWidth = 800;
            int camHeight = 600;

            // Frames por segundo.
            int FPS = 60;


            // Cargado de recursos. Recuerden que como base se les va a caer ya que no existen estos recursos de ejemplo
            // por eso está comentado. 
            
            /*myImageManager.Load("imagen.png", "imageId"); <- Cargar una imagen.
            myImageManager.Load("imagen.jpg", "imageId2"); <- Recuerde la extensión.

            myFontManager.Load("font.ttf", "fontId"); <- Cargar una fuente, formato .ttf

            myAudioManager.Load("audio.mp3", "audioId"); <- Cargar un sonido.
            myAudioManager.Load("audio.wav", "audioWav");*/

            Game game = new Game(windowWidth, windowHeight, FPS, new myCamera(0, 0, camWidth, camHeight, 
                (float)windowWidth/(float)camWidth));

            game.Start();
            
            // Esto es propio de WinForms, es básicamente para que la ventana fluya.
            Application.Run();
        }
    }
}
