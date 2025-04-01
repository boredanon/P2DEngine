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

            NewGame game = new NewGame(windowWidth, windowHeight, FPS, 
                new myCamera(0, 0, camWidth, camHeight, ((float)windowWidth / (float)camWidth)));

            
            /*Indy500 game = new Indy500(windowWidth, windowHeight, FPS,
                new myCamera(0, 0, camWidth, camHeight, ((float)windowWidth / (float)camWidth)));*/

            game.Start();
            
            // Esto es propio de WinForms, es básicamente para que la ventana fluya.
            Application.Run();
        }
    }
}
