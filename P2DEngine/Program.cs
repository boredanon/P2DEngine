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
            // Crearemos una instancia de la clase myGame, que contiene la lógica del juego.
            //PongGame game = new PongGame(800, 600, 60);
            NewGame game = new NewGame(800, 600, 60, new myCamera(0, 0, 800, 600, 1.0f));
            
            game.Start();
            
            // Esto es propio de WinForms, es básicamente para que la ventana fluya.
            Application.Run();
        }
    }
}
