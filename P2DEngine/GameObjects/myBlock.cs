using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P2DEngine
{
    //Clase para rectángulos, hereda de myGameObject.
    public class myBlock : myGameObject
    {
        public myBlock(float x, float y, float sizeX, float sizeY, Color color) : base(x, y, sizeX, sizeY, color)
        {
        }

        // ¿Cómo dibujamos un bloque?
        public override void Draw(Graphics g, Vector position, Vector size)
        {
            g.FillRectangle(brush, (float)position.X, (float)position.Y, 
                (float)size.X, (float)size.Y);
        }


        public override void Update(float deltaTime)
        {
        }
    }
}
