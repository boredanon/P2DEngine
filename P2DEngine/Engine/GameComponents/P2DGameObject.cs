using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine
{
    public abstract class P2DGameObject // Clase base para distintos objetos dentro del juego.
    {
        // Variables del objeto.

        public PointF Position { get; set; }
        public PointF Size { get; set; }

        public Color Color { get; protected set; }
        public Image Image { get; protected set; }
        
        public float Angle { get; protected set; }


        public P2DGameObject(int X, int Y, int width, int height, float Angle, Color color) 
        {
            Position = new PointF(X, Y);
            Size = new PointF(width, height);
            this.Angle = Angle; 
            Color = color;
        }

        public P2DGameObject(int X, int Y, int width, int height, float Angle, Image image)
        {
            Position = new PointF(X, Y);
            Size = new PointF(width, height);
            this.Angle = Angle;
            Color = Color.White;
            Image = image;
        }

        public abstract void Draw(Graphics g);

        public abstract void Update(float DeltaTime);

        public abstract bool IsColliding(P2DGameObject other);

    }
}
