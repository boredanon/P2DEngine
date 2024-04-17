using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine
{
    internal class P2DTriangle : P2DGameObject
    {
        // Un triángulo tiene 3 puntos.
        PointF[] points = new PointF[3];
        
        public P2DTriangle(int X, int Y, int width, int height, Color color) : base(X, Y, width, height, color){}
        public override void Update(float DeltaTime) { }

        public override void Draw(Graphics g)
        {
            if (Image == null) {
                // Un pequeño desafío: vea como puede dibujar distintos tipos de triángulos (pista: tiene que cambiar el cálculo de los puntos).
                points[0] = Position;
                points[1] = new PointF(Position.X - Size.X / 2, Position.Y + Size.Y);
                points[2] = new PointF(Position.X + Size.X / 2, Position.Y + Size.Y);

                var colorBrush = new SolidBrush(Color);
                g.FillPolygon(colorBrush, points);
            }
            else
            {
                g.DrawImage(Image, Position.X, Position.Y, Size.X, Size.Y);
            }
            
        }

        // No implementamos en la clase la colisión de triángulos.
        public override bool IsColliding(P2DGameObject other)
        {
            return false;
        }

        
    }
}
