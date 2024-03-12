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
            P2DGame game = new P2DGame(800, 600); // Inicializamos ventana, ancho x alto.
            game.Start(); // Iniciamos el juego.

            Application.Run(); // Propio de Forms, no tocar.
        }
    }
}
