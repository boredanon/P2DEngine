using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine
{
    public class P2DCircle : P2DGameObject
    {
        // Sólo color.
        public P2DCircle(int X, int Y, int radius, Color color) : base(X, Y, radius*2, radius*2, color){}
        
        // Con imagen.
        public P2DCircle(int X, int Y, int radius, Image image) : base(X, Y, radius*2, radius*2, image){}
        
        public override void Update(float DeltaTime)
        {
        }

        public override void Draw(Graphics g)
        {
            if(Image == null)
            {
                var colorBrush = new SolidBrush(Color);
                g.FillEllipse(colorBrush, Position.X, Position.Y, Size.X, Size.Y);
            }
            else
            {
                g.DrawImage(Image, Position.X, Position.Y, Size.X, Size.Y);
            }
        }


        public override bool IsColliding(P2DGameObject other)
        {
            if(other is P2DCircle) // Colisión entre dos círculos
            {
                // Recuerde como funciona Forms (desde arriba a la izquierda, entonces el radio sería el tamaño dividido en 2)
                float myRadius = Size.X / 2;
                float otherRadius = other.Size.X / 2;

                // Nos interesa el centro de los círculos.
                PointF otherCenter = new PointF(
                    other.Position.X + otherRadius,
                    other.Position.Y + otherRadius);

                PointF myCenter = new PointF(
                    Position.X + myRadius,
                    Position.Y + myRadius
                    );

                // Calculamos la distancia entre los dos centros.
                float distanceX = otherCenter.X - myCenter.X;
                float distanceY = otherCenter.Y - myCenter.Y;

                // Recuerde la fórmula de distancia entre dos puntos, en este caso queremos evitar sacar la raíz cuadrada.
                float distanceSquared = distanceX*distanceX + distanceY*distanceY;

                // Si la distancia cuadrada es menor que la suma de los radios al cuadrado.
                if(distanceSquared < (myRadius + otherRadius)*(myRadius + otherRadius))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            if(other is P2DBlock) // Colisión círculo bloque.
            {
                float myRadius = Size.X / 2;

                // Necesitamos nuevamente el centro del círculo.
                PointF myCenter = new PointF(
                    Position.X + myRadius,
                    Position.Y + myRadius);


                // Esto calcula el punto más cercano del bloque al círculo
                // Vea el archivo Imagen Explicativa 3.png para un detalle visual.
                float closestX = Math.Min(Math.Max(myCenter.X, other.Position.X), other.Position.X + other.Size.X);
                float closestY = Math.Min(Math.Max(myCenter.Y, other.Position.Y), other.Position.Y + other.Size.Y);


                // La misma lógica de la colisión círculo/círculo. Si la distancia es menor al radio, significa que el punto más cercano del rectángulo está colisionando.
                float distanceX = myCenter.X - closestX;
                float distanceY = myCenter.Y - closestY;

                float distanceSquared = distanceX * distanceX + distanceY * distanceY;

                if (distanceSquared <= myRadius * myRadius)
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
