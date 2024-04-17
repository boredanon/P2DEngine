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

            //Creamos la instancia de juego. Recuerde que le pasa la resolución y los FPS.
            Clase1604 game = new Clase1604(800, 600, 60);
            
            game.Start(); // Iniciamos el juego.

            Application.Run(); // Propio de Forms, no tocar.
        }
    }
}
