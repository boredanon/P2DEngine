using System.Windows.Forms;

namespace P2DEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PongGame game = new PongGame(800, 600, 60); // Inicializamos ventana, ancho x alto y los FPS que queremos que corra el juego.
            // Arkanoid game = new Arkanoid(800, 600, 60);
            game.Start(); // Iniciamos el juego.

            Application.Run(); // Propio de Forms, no tocar.
        }
    }
}
