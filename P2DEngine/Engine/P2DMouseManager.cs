using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2DEngine.Engine
{
    public class P2DMouseManager
    {
        public static bool isLeftButtonDown = false; // Si se ha presionado el click izquierdo.
        public static bool isRightButtonDown = false; // Si se ha presionado el click derecho.
        public static Point mouseLocation; // Posición del mouse en la pantalla de juego.

        // Si se detecta que uno de los botones del mouse ha sido presionado.
        public static void MouseDown(MouseButtons b)
        {
            if(b == MouseButtons.Left && !isLeftButtonDown) // Si se presionó el izquierdo.
            {
                isLeftButtonDown = true;
            }
            else if(b == MouseButtons.Right && !isRightButtonDown) // Si se presionó el derecho.
            {
               isRightButtonDown = true;
            }
        }

        // Si se detecta que uno de los botones del mouse ha sido soltado.
        public static void MouseUp(MouseButtons b) { 
            if(b == MouseButtons.Left && isLeftButtonDown) // Si fue el izquierdo.
            {
                isLeftButtonDown = false;
            }
            else if(b == MouseButtons.Right && isRightButtonDown) // Si fue el derecho.
            {
                isRightButtonDown = false;
            }
        }

        // Si se detecta movimiento del mouse dentro de la pantalla.
        public static void MouseMove (Point location)
        {
            mouseLocation = location;
        }
    }
}
