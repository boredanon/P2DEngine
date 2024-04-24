using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using P2DEngine.Engine;

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

            // Propio de forms, para obtener los datos del mouse.
            MouseMove += _MouseMove;
            MouseDown += _MouseDown;
            MouseUp += _MouseUp;

        }

        // No les recomiendo tocar los métodos de este archivo.
        public Graphics GetGraphics()
        {
            ManagedBackBuffer.Graphics.Clear(Color.Black);
            return ManagedBackBuffer.Graphics;
        }

        public void Render()
        {
            if(ManagedBackBuffer != null)
                ManagedBackBuffer?.Render();
        }

        // Para evaluar si una tecla se presionó o se soltó.
        private void _KeyDown(object sender, KeyEventArgs e)
        {
            P2DInputManager.KeyDown(e.KeyCode);
        }

        private void _KeyUp(object sender, KeyEventArgs e)
        {
            P2DInputManager.KeyUp(e.KeyCode);
        }

        // Para evaluar los datos del mouse.
        private void _MouseDown(object sender, MouseEventArgs e)
        {
            P2DMouseManager.MouseDown(e.Button);
        }

        private void _MouseUp(object sender, MouseEventArgs e)
        {
            P2DMouseManager.MouseUp(e.Button);
        }

        private void _MouseMove(object sender, MouseEventArgs e)
        {
            P2DMouseManager.MouseMove(e.Location);
        }

        public void ChangeSize(Form window, int width, int height)
        {
            if(window.InvokeRequired)
            {
                window.Invoke(new MethodInvoker(
                    delegate {
                        window.ClientSize = new Size(width, height);
                        GraphicsManager.MaximumBuffer = new Size((width + 1), (height + 1));
                        ManagedBackBuffer = GraphicsManager.Allocate(CreateGraphics(), ClientRectangle);
                    }));
            }
        }

        public void ChangeWidth(Form window, int width)
        {
            if (window.InvokeRequired)
            {
                window.Invoke(new MethodInvoker(
                    delegate {
                        var height = (width * window.ClientSize.Height) / window.ClientSize.Width;
                        window.ClientSize = new Size(width, height);
                        GraphicsManager.MaximumBuffer = new Size((width + 1), (height + 1));
                        ManagedBackBuffer = GraphicsManager.Allocate(CreateGraphics(), ClientRectangle);
                    }));
            }
        }

        public void ChangeHeight(Form window, int height)
        {
            if (window.InvokeRequired)
            {
                window.Invoke(new MethodInvoker(
                    delegate {
                        var width = (height * window.ClientSize.Width) / window.ClientSize.Height;
                        window.ClientSize = new Size(width, height);
                        GraphicsManager.MaximumBuffer = new Size((width + 1), (height + 1));
                        ManagedBackBuffer = GraphicsManager.Allocate(CreateGraphics(), ClientRectangle);
                    }));
            }
        }

    }
}
