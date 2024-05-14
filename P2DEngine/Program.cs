using P2DEngine.Engine;
using P2DEngine.Games;
using P2DEngine.Games.Clases;
using System;
using System.Windows.Forms;

namespace P2DEngine
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Cargamos los archivos correspondientes. Consulte las clases P2DImageManager y P2DAudioManager para saber donde tienen que ir ubicados los archivos.
            P2DImageManager.Load("L1.jpeg", "Background");
            P2DImageManager.Load("Green.png", "Green");
            P2DImageManager.Load("Blue.png", "Blue");

            P2DAudioManager.Load("Bouncing.wav", "bounce");
            P2DAudioManager.Load("Background.wav", "background_music");

            P2DImageManager.Load("Parallax/bg_1.png", "bg_1");
            P2DImageManager.Load("Parallax/bg_2.png", "bg_2");
            P2DImageManager.Load("Parallax/bg_3.png", "bg_3");
            P2DImageManager.Load("Parallax/bg_4.png", "bg_4");


            P2DImageManager.Load("Test/A.png", "player_0");
            P2DImageManager.Load("Test/B.png", "player_1");
            P2DImageManager.Load("Test/C.png", "player_2");
            P2DImageManager.Load("Test/D.png", "other_0");
            P2DImageManager.Load("Test/E.png", "other_1");




            //Creamos la instancia de juego. Recuerde que le pasa la resolución y los FPS. Ahora también le pasamos el tamaño del viewport.
            Clase1405 game = new Clase1405(800, 600, new P2DViewport(0, 0, 800, 600), 60);
            
            game.Start(); // Iniciamos el juego.

            Application.Run(); // Propio de Forms, no tocar.
        }
    }
}
