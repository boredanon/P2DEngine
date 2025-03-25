using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace P2DEngine
{
    // Clase que tiene la lógica de la ventana.
    public class myWindow : Form
    {
        // Para el doble búfer.
        BufferedGraphicsContext GraphicsManager;
        BufferedGraphics managedBackBuffer;

        public myWindow(int width, int height)
        { 
            ClientSize = new Size(width, height); // El cliente, vendría siendo la parte de la ventana dentro de los márgenes.
            MaximizeBox = false; // Permitir que se maximice la ventana.
            FormBorderStyle = FormBorderStyle.FixedSingle; // Decidir si se puede cambiar el tamaño de la ventana.

            // Véalo como un búfer fantasma, le cargamos en memoria todo lo que dibujamos
            // y luego lo mostramos en pantalla.
            GraphicsManager = BufferedGraphicsManager.Current;
            GraphicsManager.MaximumBuffer = new Size(width, height);
            managedBackBuffer = GraphicsManager.Allocate(CreateGraphics(), ClientRectangle);


            // Necesitamos añadirlos para que la ventana "escuche" a las presiones del teclado. Sin esto el programa no sabría
            // como recibir los inputs.
            KeyDown += _KeyDown;
            KeyUp += _KeyUp;

            MouseDown+= _MouseDown;
            MouseUp+= _MouseUp;
            MouseMove+= _MouseMove;
        }

        public Graphics GetGraphics()
        {
            managedBackBuffer.Graphics.Clear(Color.Black);
            return managedBackBuffer.Graphics;

        }

        public void Render()
        {
            managedBackBuffer.Render();
        }

        // Presionar un botón del mouse.
        public void _MouseDown(object sender, MouseEventArgs e)
        {
            myInputManager.MouseDown(e.Button);
        }

        // Levantar un botón del mouse.
        public void _MouseUp(object sender, MouseEventArgs e)
        {
            myInputManager.MouseUp(e.Button);
        }

        // Mover el mouse.
        public void _MouseMove(object sender, MouseEventArgs e)
        {
            myInputManager.MouseMove(e.Location);
        }

        // Presionar una tecla.
        public void _KeyDown(object sender, KeyEventArgs e)
        {
            myInputManager.KeyDown(e.KeyCode);
        }

        // Levantar una tecla.
        public void _KeyUp(object sender, KeyEventArgs e)
        {
            myInputManager.KeyUp(e.KeyCode);
        }


       
    }
}
