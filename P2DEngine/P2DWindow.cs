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
        private List<Keys> pressedKeys;

        public P2DWindow(int width, int height) {

            pressedKeys = new List<Keys>(); // Lista de teclas presionadas.

            // Propio de Forms.
            ClientSize = new Size(width, height); // Tamaño de la ventana.
            MaximizeBox = false; // Pantalla completa.
            FormBorderStyle = FormBorderStyle.FixedSingle; // Esto hace que aparezca la barra de arriba para minimizar,
            //cerrar, etc.

            // Propio de Forms.
            KeyDown += _KeyDown;
            KeyUp += _KeyUp;

        }

        private void _KeyDown(object sender, KeyEventArgs e)
        {
            if (!pressedKeys.Contains(e.KeyCode)) // Si no está presionando la tecla.
            {
                pressedKeys.Add(e.KeyCode); // Ahora la está presionando.
            }
        }

        private void _KeyUp(object sender, KeyEventArgs e)
        {
            if (pressedKeys.Contains(e.KeyCode)) // Si estaba presionando la tecla.
            {
                pressedKeys.Remove(e.KeyCode); // Ahora no la está presionando.
            }
        }

        public bool IsKeyPressed(Keys keys) // Preguntamos si está la tecla presionada.
        {
            return pressedKeys.Contains(keys);
        }

    }
}
