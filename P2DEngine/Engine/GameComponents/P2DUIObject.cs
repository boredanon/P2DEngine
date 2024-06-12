using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine.GameComponents
{
    // Los UIObject funcionan casi igual a los GameObject, les quitamos algunas cosas que no queremos
    // como la colisión.
    public abstract class P2DUIObject
    {
        public PointF Position;
        public PointF Size;

        public float Angle;

        public Color Color { get; protected set; }
        public Image Image { get; protected set; }

        public P2DUIObject(int X, int Y, int width, int height, float angle, Color color) { 
            Position = new PointF(X, Y);
            Size = new PointF(width, height);
            Angle = angle;
            Color = color;
        }

        public P2DUIObject(int X, int Y, int width, int height, float angle, Image image)
        {
            Position = new PointF(X, Y);
            Size = new PointF(width, height);
            Angle = angle;
            Image = image;
        }

        public abstract void Draw(Graphics g);

        public abstract void Update(float DeltaTime);
    }
}
