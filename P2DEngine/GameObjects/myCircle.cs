using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P2DEngine
{
    //Al igual que la clase myBlock, esta hereda de myGameObject, para poder dibujar círculos.
    public class myCircle : myGameObject
    {
        public myCircle(float x, float y, float sizeX, float sizeY, Color color) : base(x, y, sizeX, sizeY, color)
        {
        }

        // ¿Cómo dibujamos un círculo?
        public override void Draw(Graphics g, Vector position, Vector size)
        {
            g.FillEllipse(brush, (float)position.X, (float)position.Y, (float)size.X, (float)size.Y);
        }

        public override void Update(float deltaTime)
        {
        }
    }
}
