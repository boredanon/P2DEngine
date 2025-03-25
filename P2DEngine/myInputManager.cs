using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2DEngine
{
    // Clase que maneja todo lo referente al input.
    public static class myInputManager
    {
        public static List<Keys> pressedKeys = new List<Keys>(); // Teclado

        // Mouse.
        public static PointF mousePosition;
        public static bool isLeftButtonDown;
        public static bool isRightButtonDown;

        // Botón del mouse presionado.
        public static void MouseDown(MouseButtons e)
        {
            if (e == MouseButtons.Left && !isLeftButtonDown)
            {
                isLeftButtonDown = true;
            }
            if(e == MouseButtons.Right && !isRightButtonDown)
            {
                isRightButtonDown = true;
            }
        }

        // Botón del mouse levantado.
        public static void MouseUp(MouseButtons e)
        {
            if (e == MouseButtons.Left && isLeftButtonDown)
            {
                isLeftButtonDown = false;
            }
            if (e == MouseButtons.Right && isRightButtonDown)
            {
                isRightButtonDown = false;
            }
        }

        // Movimiento del mouse.
        public static void MouseMove(Point mouseLocation)
        {
            mousePosition = mouseLocation;
        }

        // Presionar una tecla.
        public static void KeyDown(Keys key)
        {
            if (!pressedKeys.Contains(key))
            {
                pressedKeys.Add(key);
            }
        }

        // Levantar una tecla.
        public static void KeyUp(Keys key)
        {
            if (pressedKeys.Contains(key))
            {
                pressedKeys.Remove(key);
            }
        }

        // ¿Está la tecla presionada?
        public static bool IsKeyPressed(Keys e)
        {
            return pressedKeys.Contains(e);
        }
    }
}