using P2DEngine.Engine.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace P2DEngine.Engine.GameComponents
{
    //Clase para cada capa del fondo.
    public class P2DBackgroundLayer
    {
        Image image; // Imagen correspondiente a la capa.

        public PointF Position; // Posición, es relevante cuando utilizamos el efecto parallax.
        public PointF Size; // Tamaño, es relevante cuando cambiamos de tamaño el viewport.

        public float speed; // Velocidad para el movimiento parallax
        public float speedScale; // Escala asociada, recuerde que mientras más cerca al ojo, se mueve más rápido.

        public P2DBackgroundLayer(Image image, float speedScale)
        {
            this.image = image;

            Size = P2DGame.viewportSize;
            this.speedScale = speedScale;
            speed = P2DBackgroundManager.parallaxSpeed * speedScale; // Asignamos la velocidad escalada.
        }

        public void Update(float DeltaTime)
        {
            //Recuerde: si quiere un background estático, simplemente mantenga speed = 0.
            Position = new PointF(Position.X + speed * DeltaTime, Position.Y);

            //Para hacer que se ciclen las imágenes, dando un efecto de fondo "infinito"
            //Simplemente volviendo hacia atrás si se pasa de cierto punto.
            if (Position.X > P2DGame.viewportSize.X)
            {
                Position.X -= Size.X;
            }
            else if (Position.X < 0)
            {
                Position.X += Size.X;
            }
        }

        public void Draw(Graphics g)
        {
            //El truco de hacer que parezca infinito se puede hacer de distintas formas.
            //La más sencilla es dibujar la imagen tres veces de manera seguida.
            g.DrawImage(image, Position.X - Size.X + 2, Position.Y, Size.X, Size.Y);
            g.DrawImage(image, Position.X, Position.Y, Size.X, Size.Y);
            g.DrawImage(image, Position.X + Size.X - 2, Position.Y, Size.X, Size.Y);

            
        }

        

    }
}
