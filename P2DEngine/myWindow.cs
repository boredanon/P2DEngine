using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace P2DEngine
{
    // Clase que tiene la lógica de la ventana.
    public class myWindow : Form
    {
        // Lista de teclas presionadas.
        List<Keys> pressedKeys;

        public myWindow(int width, int height)
        {
            pressedKeys = new List<Keys>(); // Inicializamos la lista.


            ClientSize = new Size(width, height); // El cliente, vendría siendo la parte de la ventana dentro de los márgenes.
            MaximizeBox = false; // Permitir que se maximice la ventana.
            FormBorderStyle = FormBorderStyle.FixedSingle; // Decidir si se puede cambiar el tamaño de la ventana.

            // Necesitamos añadirlos para que la ventana "escuche" a las presiones del teclado. Sin esto el programa no sabría
            // como recibir los inputs.
            KeyDown += _KeyDown;
            KeyUp += _KeyUp;
        }

        // Presionar una tecla.
        public void _KeyDown(object sender, KeyEventArgs e)
        {
            if (!pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Add(e.KeyCode);
            }
        }

        // Levantar una tecla.
        public void _KeyUp(object sender, KeyEventArgs e)
        {
            if (pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Remove(e.KeyCode);
            }
        }

        // ¿Está la tecla presionada?
        public bool IsKeyPressed(Keys e)
        {
            return pressedKeys.Contains(e);
        }
    }
}
