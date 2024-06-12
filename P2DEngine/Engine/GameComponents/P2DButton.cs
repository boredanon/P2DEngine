using P2DEngine.Engine.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine.GameComponents
{
    public class P2DButton : P2DUIObject
    {
        // Tres estados principales de un botón: presionado, no presionado y que el mouse esté encima.
        public bool isDown;
        public bool isUp;
        public bool isHover;

        // Texto asociado al botón.
        public string buttonText;

        // DESAFÍO: Que el botón tenga una imagen en vez de texto, revise los P2DGameObject para obtener una idea.

        public P2DButton(int X, int Y, int width, int height, float angle, Color color) : base(X, Y, width, height, angle, color)
        {
            buttonText = "button";

        }

        public override void Draw(Graphics g)
        {
            // Para mostrar los estados, dibujamos el botón de distinto color para cada estado.
            if(isDown)
            {
                g.FillRectangle(new SolidBrush(Color.Blue), Position.X, Position.Y, Size.X, Size.Y);
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.Yellow), Position.X, Position.Y, Size.X, Size.Y);
            }

            // DESAFÍO: Pruebe a cambiar el tamaño de la pantalla, ¿cómo podríamos adaptar el tamaño de la fuente?
            Font font = new Font("Arial", 14);
            g.DrawString(buttonText, font, new SolidBrush(Color.Black), Position.X, Position.Y);
        }

        public override void Update(float DeltaTime)
        {
            var mousePosition = P2DMouseManager.mouseLocation;
           
            // Si el mouse está dentro de los límites del botón.
            if (mousePosition.X > Position.X && mousePosition.X < Position.X + Size.X &&
                 mousePosition.Y > Position.Y && mousePosition.Y < Position.Y + Size.Y)
            {
                isHover = true; 
                if (P2DMouseManager.isLeftButtonDown) // Si está dentro de los límites y el botón está siendo presionado.
                {
                    isDown = true;
                    isUp = false;
                    buttonText = "Down";
                }
                else
                {
                    buttonText = "Hover";
                }
            }
            else // Fuera de los límites del botón.
            { 
                buttonText = "Up";
                isUp = true;
                isDown = false;
            }
        }
    }
}
