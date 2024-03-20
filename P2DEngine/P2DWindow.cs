using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace P2DEngine
{
    public class P2DWindow : Form
    { 

        //Para el double búfer.
        BufferedGraphicsContext GraphicsManager;
        BufferedGraphics ManagedBackBuffer;

        public P2DWindow(int width, int height) {

            // Propio de Forms.
            ClientSize = new Size(width, height); // Tamaño de la ventana.
            MaximizeBox = false; // Pantalla completa.
            FormBorderStyle = FormBorderStyle.FixedSingle; // Esto hace que aparezca la barra de arriba para minimizar,
            //cerrar, etc.

            // Doble búfer. Si tocan el código se pudre todo.
            GraphicsManager = BufferedGraphicsManager.Current;
            GraphicsManager.MaximumBuffer = new Size(width + 1, height + 1);
            ManagedBackBuffer = GraphicsManager.Allocate(CreateGraphics(), ClientRectangle);

            // Propio de Forms.
            KeyDown += _KeyDown;
            KeyUp += _KeyUp;

        }

        public Graphics GetGraphics()
        {
            ManagedBackBuffer.Graphics.Clear(Color.Black);
            return ManagedBackBuffer.Graphics;
        }

        public void Render()
        {
            ManagedBackBuffer?.Render();
        }

        private void _KeyDown(object sender, KeyEventArgs e)
        {
            P2DInputManager.KeyDown(e.KeyCode);
        }

        private void _KeyUp(object sender, KeyEventArgs e)
        {
            P2DInputManager.KeyUp(e.KeyCode);
        }

    }
}
