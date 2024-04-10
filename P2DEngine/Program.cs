using P2DEngine.Engine;
using P2DEngine.Games.Arkanoid;
using System.Windows.Forms;

namespace P2DEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Cargamos los archivos correspondientes. Consulte las clases P2DImageManager y P2DAudioManager para saber donde tienen que ir ubicados los archivos.
            P2DImageManager.Load("L1.jpeg", "Background");
            P2DImageManager.Load("Green.png", "Green");
            P2DImageManager.Load("Blue.png", "Blue");

            P2DAudioManager.Load("Bouncing.wav", "bounce");

            P2DAudioManager.Load("Background.wav", "background_music");

            //Arkanoid game = new Arkanoid(800, 600, 60);
            MouseControl game = new MouseControl(800, 600, 60);
            
            game.Start(); // Iniciamos el juego.

            Application.Run(); // Propio de Forms, no tocar.
        }
    }
}
