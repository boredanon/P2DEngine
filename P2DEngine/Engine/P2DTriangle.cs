using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine
{
    internal class P2DTriangle : P2DGameObject
    {
        // Un triángulo tiene 3 puntos.
        PointF[] points = new PointF[3];
        
        public P2DTriangle(int X, int Y, int width, int height, float angle, Color color) : base(X, Y, width, height, angle, color){}
        public override void Update(float DeltaTime) { }

        public override void Draw(Graphics g)
        {
            if (Image == null) {
                // Un pequeño desafío: vea como puede dibujar distintos tipos de triángulos (pista: tiene que cambiar el cálculo de los puntos).
                points[0] = Position;
                points[1] = new PointF(Position.X - Size.X / 2, Position.Y + Size.Y);
                points[2] = new PointF(Position.X + Size.X / 2, Position.Y + Size.Y);

                var colorBrush = new SolidBrush(Color);
                using (Matrix m = new Matrix()) // Todas las operaciones que hacemos en realidad son operaciones matriciales encubiertas, en este caso declaramos la matriz identidad.
                {
                    m.RotateAt(Angle,
                        new PointF(Position.X + (Size.X / 2),
                        Position.Y + (Size.Y / 2))); // Rotamos esa matriz identidad (que en realidad es una multiplicación de matrices) con respecto al punto del medio.

                    g.Transform = m; // Básicamente, rotamos todo el mundo.
                    g.FillPolygon(colorBrush, points); // Dibujamos en el mundo rotado.
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

        // No implementamos en la clase la colisión de triángulos, pero tiene la opción de hacerlo usted. El triángulo es el polígono más simple, si logra hacerlo, tendrá la información necesaria para hacer colisionar cualquier figura.
        public override bool IsColliding(P2DGameObject other)
        {
            return false;
        }

        
    }
}
