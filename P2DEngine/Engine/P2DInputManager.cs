using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2DEngine
{
    // Clase que maneja todas las entradas el jugador.
    public class P2DInputManager
    {
        public static List<Keys> pressedKeys = new List<Keys>(); // Lista de teclas presionadas.

        public static bool IsKeyPressed(Keys keys) // Preguntamos si está la tecla presionada.
        {
            return pressedKeys.Contains(keys);
        }

        public static void KeyDown(Keys key)
        {
            if (!pressedKeys.Contains(key)) // Si no está presionando la tecla.
            {
                pressedKeys.Add(key); // Ahora la está presionando.
            }
        }

        public static void KeyUp(Keys key)
        {
            if (pressedKeys.Contains(key)) // Si estaba presionando la tecla.
            {
                pressedKeys.Remove(key); // Ahora no la está presionando.
            }
        }
    }
}
