using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine
{
    public class P2DBlock : P2DGameObject
    {
        // Solo colores.
        public P2DBlock(int X, int Y, int width, int height, float angle, Color color) : base(X, Y, width, height, angle, color){}
        // Con imágenes
        public P2DBlock(int X, int Y, int width, int height, float angle, Image image) : base(X, Y, width, height, angle, image){}


        public override void Draw(Graphics g)
        {
            if (Image == null)
            {
                var colorBrush = new SolidBrush(Color);

                using(Matrix m = new Matrix()) // Todas las operaciones que hacemos en realidad son operaciones matriciales encubiertas, en este caso declaramos la matriz identidad.
                {   
                    m.RotateAt(Angle, 
                        new PointF(Position.X + (Size.X/2), 
                        Position.Y + (Size.Y/2))); // Rotamos esa matriz identidad (que en realidad es una multiplicación de matrices) con respecto al punto del medio.
                   
                    g.Transform = m; // Básicamente, rotamos todo el mundo.
                    g.FillRectangle(colorBrush, Position.X, Position.Y, Size.X, Size.Y); // Dibujamos en el mundo rotado.
                    g.ResetTransform(); // Volvemos el mundo a la normalidad.
                }

                
            }
            else
            {
                using (Matrix m = new Matrix()) // Las imágenes siguen la misma lógica.
                {
                    m.RotateAt(Angle,
                        new PointF(Position.X + (Size.X / 2),
                        Position.Y + (Size.Y / 2)));

                    g.Transform = m;
                    g.DrawImage(Image, Position.X, Position.Y, Size.X, Size.Y);
                    g.ResetTransform();
                }

            }
        }

        public override void Update(float DeltaTime)
        {

        }

        public override bool IsColliding(P2DGameObject other)
        {
            if(other is P2DBlock) // Colisión rectángulo/rectángulo.
            {
                if(Position.X + Size.X > other.Position.X && 
                    Position.X <= other.Position.X + other.Size.X &&
                    Position.Y + Size.Y > other.Position.Y &&
                    Position.Y <= other.Position.Y + other.Size.Y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (other is P2DCircle) // Colisión rectángulo/círculo, vea la clase P2DCircle.
            {
                float otherRadius = other.Size.X / 2;

                PointF otherCenter = new PointF(
                    other.Position.X + otherRadius,
                    other.Position.Y + otherRadius);

                float closestX = Math.Min(Math.Max(otherCenter.X, Position.X), Position.X + Size.X);
                float closestY = Math.Min(Math.Max(otherCenter.Y, Position.Y), Position.Y + Size.Y);

                float distanceX = otherCenter.X - closestX;
                float distanceY = otherCenter.Y - closestY;
            
                float distanceSquared = distanceX * distanceX + distanceY * distanceY;

                if(distanceSquared <= otherRadius*otherRadius)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
            }
            return false;
        }
    }
}
